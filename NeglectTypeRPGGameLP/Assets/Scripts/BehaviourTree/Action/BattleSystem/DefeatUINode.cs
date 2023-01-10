using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class DefeatUINode : ActionNode
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
            LoseUI();
            return State.Success;
        }

        public void LoseUI()
        {
            BSC.EndUI[1].SetActive(true);
        }
    }
}