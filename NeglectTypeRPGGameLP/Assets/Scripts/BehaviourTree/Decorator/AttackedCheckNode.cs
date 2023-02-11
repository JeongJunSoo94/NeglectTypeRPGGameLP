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
        }

        protected override State OnUpdate()
        {
            if (child.Update() == State.Success)
            {
                context.info.isDamaged = false;
                return State.Failure;
            }
            return State.Success;
        }
    }
}
