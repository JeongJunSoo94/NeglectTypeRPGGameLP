using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class AttackedCheckNode : DecoratorNode
    {
        HeroContext context;
        float curCount;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
        }

        protected override void OnStop()
        {
            context.info.damageCount--;
        }

        protected override State OnUpdate()
        {
            if (context.info.damageCount>0)
                return child.Update();
            return State.Failure;
        }
    }
}
