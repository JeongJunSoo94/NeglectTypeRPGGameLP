using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JJS.BT;

namespace NeglectTypeRPG
{
    public class AttackNode : ActionNode, ICharacterNode
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
            for (int i = 0; i < context.targets.Count; ++i)
            {
                context.targets[i].GetComponent<HeroContext>().info.Damaged(context.info,0,true,0);
            }
        }
    }
}