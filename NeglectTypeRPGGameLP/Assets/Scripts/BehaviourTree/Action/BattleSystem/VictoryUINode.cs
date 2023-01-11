using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class VictoryUINode : ActionNode
    {
        BattleSystemContext bsc; 
        public bool isOn;
        protected override void OnStart()
        {
            if (bsc == null)
                bsc = blackBoard.context as BattleSystemContext;
            VictoryUI(isOn);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
        public void VictoryUI(bool value)
        {
            bsc.endUI[0].SetActive(value);
        }
    }
}