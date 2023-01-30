using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public enum PositionType
    {
        None,
        OriginPos,
        TargetFront,
        TargetBack,
        TargetLeft,
        TargetRight,
        FrontCenter,
        BackCenter,
        SideFront,
        SideCenter,
    }
    public enum MoveType
    {
        None,
        MoveTowards,
        Teleportation,
    }
    public class MoveNode : ActionNode,ICharacterNode
    {
        HeroContext context;
        public PositionType positionType;
        public MoveType moveType;
        bool success;
        float distance; 
        public float speed = 10;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            context.targetPos = context.targets[0].transform.position;
            distance = context.gameObject.transform.localScale.x + context.targets[0].transform.localScale.x;//Vector3.Distance(context.gameObject.transform.position ,context.targetPos);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if(success)
                return State.Success;
            return Action();

        }

        public State Action()
        {
            context.gameObject.transform.LookAt(new Vector3(context.targetPos.x, context.gameObject.transform.position.y, context.targetPos.z));
            context.gameObject.transform.position += speed * context.gameObject.transform.forward * Time.deltaTime;

            if (Vector3.Distance(context.gameObject.transform.position, context.targetPos) <= distance)
            {
                return State.Success;
            }
            return State.Running;
        }
    }
}