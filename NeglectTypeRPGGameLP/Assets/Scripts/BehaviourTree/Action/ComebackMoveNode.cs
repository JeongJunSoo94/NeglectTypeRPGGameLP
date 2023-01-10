using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class ComebackMoveNode : ActionNode
    {
        HeroContext context;
        HeroBlackBoard hbb; 
        public bool isMove;
        bool success;

        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
                hbb = blackBoard as HeroBlackBoard;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return Action();

        }

        public void TargetLockOn()
        {
            //if (context.isRed)
            //{
            //    for (int i = 0; i < context.bsc.BlueHero.Count; i++)
            //    {
            //        if (context.bsc.BlueHero[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
            //        {
            //            context.target = context.bsc.BlueHero[i];
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < context.bsc.RedHero.Count; i++)
            //    {
            //        if (context.bsc.RedHero[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
            //        {
            //            context.target = context.bsc.RedHero[i];
            //            break;
            //        }
            //    }
            //}
            isMove = true;
            Debug.Log("Å¸°Ù ·Ï¿Â" + context.target);
        }

        public State Action()
        {
            context.gameObject.transform.position = Vector3.Lerp(context.gameObject.transform.position, context.originPos, 0.05f);

            if (Vector3.Distance(context.gameObject.transform.position, context.originPos) <= 10.0f)
            {
                context.myTurn = false;
                BattleSystemContext con = hbb.battleSystemBlackboard.context as BattleSystemContext;
                con.state = BattleState.Battle;
                return State.Success;
            }
            return State.Running;
        }
    }
}