using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class StateChangeNode : ActionNode, ISystemNode
    {
        BattleSystemContext BSC;
        public BattleState battleState;
        protected override void OnStart()
        {
            if (BSC == null)
            {
                BSC = blackBoard.context as BattleSystemContext;
            }
        }

        protected override void OnStop()
        {
            BSC.state = battleState;
        }

        protected override State OnUpdate()
        {
            return State.Failure;
        }
    }
}