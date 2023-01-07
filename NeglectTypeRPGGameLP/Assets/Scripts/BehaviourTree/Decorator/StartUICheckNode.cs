using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class StartUICheckNode : DecoratorNode
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
            if (BSC.state == BattleState.Start && child.Update()==State.Success)
            {
                BSC.state = BattleState.Battle;
                BSC.BattleUI(false);
                return State.Failure;
            }
            return State.Running;
        }
    }
}