using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class HelthCheckNode : DecoratorNode
    {
        HeroContext HC;
        protected override void OnStart()
        {
            if (HC == null)
            {
                HC = blackBoard.context as HeroContext;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (HC.info.curHealth <= 0)
            {
                return child.Update();
            }
            else
                return State.Failure;
        }
    }
}