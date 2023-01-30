using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class ComebackMoveNode : ActionNode, ICharacterNode
    {
        HeroContext context;
        HeroBlackBoard hbb; 
        public bool isMove;
        bool success;
        float distance;
        public float speed=10;
        Team myTeam;
        protected override void OnStart()
        {
            if (myTeam == Team.None)
            {
                myTeam = blackBoard.data.TeamCheck(blackBoard);
            }
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
                hbb = blackBoard as HeroBlackBoard;
                speed = 10;
            }

            distance = Vector3.Distance(context.gameObject.transform.position, context.targetPos) * 0.05f;
        }

        protected override void OnStop()
        {
            context.info.heroUI();
        }

        protected override State OnUpdate()
        {
            return Action();

        }

        public void TargetLockOn()
        {
            isMove = true;
        }

        public State Action()
        {
            context.gameObject.transform.LookAt(new Vector3(context.originPos.x, context.gameObject.transform.position.y, context.originPos.z));
            context.gameObject.transform.position += speed * context.gameObject.transform.forward* Time.deltaTime;//Vector3.Lerp(context.gameObject.transform.position, context.originPos, Time.deltaTime);

            if (Vector3.Distance(context.gameObject.transform.position, context.originPos) <= distance)
            {
                context.myTurn = false;
                BattleSystemContext con = hbb.battleSystemBlackboard.context as BattleSystemContext;
                con.state = BattleState.Battle;
                if(myTeam == Team.RED)
                    context.gameObject.transform.rotation=Quaternion.Euler(0,180,0);
                else
                    context.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                return State.Success;
            }
            return State.Running;
        }
    }
}