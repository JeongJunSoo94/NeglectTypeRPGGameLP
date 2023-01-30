using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class StartUINode : ActionNode, ISystemNode
    {
        BattleSystemContext BSC;
        public bool isOn;
        protected override void OnStart()
        {
            if (BSC == null)
            {
                BSC = blackBoard.context as BattleSystemContext;
            }
            StartUI(isOn);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
        public void StartUI(bool value)
        {
            for (int i = 0; i < BSC.startUI.Length; ++i)
            { 
                BSC.startUI[i].SetActive(value);
            }
        }


    }
}