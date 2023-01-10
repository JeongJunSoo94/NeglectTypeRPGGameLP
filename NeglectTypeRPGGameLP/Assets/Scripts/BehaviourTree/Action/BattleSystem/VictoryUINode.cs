using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class VictoryUINode : ActionNode
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
            if (BSC.state == BattleState.End)
            {
                VictoryUI();
                return State.Running;
            }
            return State.Success;
        }
        public void VictoryUI()
        {
            BSC.EndUI[0].SetActive(true);
        }
    }
}