using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JJS.BT;

namespace NeglectTypeRPG
{
    public class SyncCheckNode : DecoratorNode
    {
        HeroContext context;
        public int syncIndex;
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
            if (context.syncBehavior== syncIndex)
            {
                //child.Update();
                return child.Update();
            }
            return State.Running;
        }

    }
}