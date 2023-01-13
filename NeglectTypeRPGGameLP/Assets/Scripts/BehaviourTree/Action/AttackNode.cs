using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JJS.BT
{
    public class AttackNode : ActionNode
    {
        HeroContext context;

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
            Attack();
            return State.Success;
        }

        void Attack()
        {
            context.target.GetComponent<HeroContext>().info.Damaged(context.info);
        }
    }
}