using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class MoveNode : ActionNode
    {
        HeroContext context;

        public bool isMove;
        bool success;

        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
           
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if(success)
                return State.Success;
            return Action();

            return State.Failure;
        }

        public State Action()
        {
            context.gameObject.transform.position = Vector3.Lerp(context.gameObject.transform.position, context.target.transform.position, 0.05f);

            if (Vector3.Distance(context.gameObject.transform.position, context.target.transform.position)<=50.0f)
            {
                isMove = false;
                return State.Success;
            }
            return State.Running;
        }
    }
}