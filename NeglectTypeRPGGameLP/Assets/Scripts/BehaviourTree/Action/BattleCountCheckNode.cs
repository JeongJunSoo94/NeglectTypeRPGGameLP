using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleCountCheckNode : ActionNode
    {
        BattleSystemContext BSC;
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
            if (BSC.state == BattleState.Battle)
            {

                return State.Success;
            }
            return State.Failure;
        }
    }
}