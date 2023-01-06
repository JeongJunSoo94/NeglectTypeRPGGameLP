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
                switch (child.Update())
                {
                    case State.Running:
                        { 
                            return State.Running;
                        }
                    case State.Success:
                        {
                            return State.Success;
                        }
                }
            }
            return State.Failure;
        }
    }
}