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
                isMove = false;
                return State.Success;
            }
            return State.Running;
        }
    }
}