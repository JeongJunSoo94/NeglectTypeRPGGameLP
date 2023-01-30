using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class StateCheckNode : DecoratorNode
    {
        BattleSystemContext BSC;
        public BattleState checkState;
        protected override void OnStart()
        {
            if (BSC == null)
            {
                BSC = blackBoard.context as BattleSystemContext;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (BSC.state == checkState)
            {
                if(child.Update()==State.Success)
                    return State.Failure;
                return State.Running;
            }
            return State.Failure;
        }
    }
}