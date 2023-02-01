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
        public bool repeat;
        bool check;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            check = true;
        }
        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (context.syncBehavior== syncIndex)
            {
                if (repeat)
                {
                    if (check)
                    {
                        check = false;
                        child.Update();
                    }    
                    return State.Failure;
                }
                else
                    return child.Update();
            }
            return State.Running;
        }

    }
}