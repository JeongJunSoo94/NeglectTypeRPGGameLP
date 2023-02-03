using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class SkillNode : ActionNode
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
                context.targets[i].GetComponent<HeroContext>().info.SkillDamaged(context.info,0);
            }
        }
    }
}