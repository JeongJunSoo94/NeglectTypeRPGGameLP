using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class VictoryCheckNode : DecoratorNode
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

            if (HC.win)
            {
                child.Update();
            }
            return State.Failure;
        }
    }
}