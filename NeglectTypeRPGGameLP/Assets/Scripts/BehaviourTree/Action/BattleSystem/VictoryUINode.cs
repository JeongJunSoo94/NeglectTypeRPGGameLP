using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class VictoryUINode : ActionNode, ISystemNode
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
            if (bsc.endUI[0].activeSelf)
                return State.Running;
            return State.Success;
        }
        public void VictoryUI(bool value)
        {
            bsc.endUI[0].SetActive(value);
        }
    }
}