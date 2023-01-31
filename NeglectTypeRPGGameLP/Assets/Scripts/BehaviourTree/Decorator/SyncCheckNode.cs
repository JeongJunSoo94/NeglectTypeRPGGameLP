using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JJS.BT;

namespace NeglectTypeRPG
{
    public class SyncCheckNode : DecoratorNode
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
            context.syncBehavior = false;
        }

        protected override State OnUpdate()
        {
            if (context.syncBehavior)
            {
                child.Update();
                return State.Success;
            }
            return State.Running;
        }

    }
}