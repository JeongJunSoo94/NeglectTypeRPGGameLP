using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class ReadyUINode : ActionNode
    {
        BattleSystemContext bsc;
        public bool isOn;
        protected override void OnStart()
        {
            if (bsc == null)
                bsc = blackBoard.context as BattleSystemContext;
            ReadyUI(isOn);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

        public void ReadyUI(bool value)
        {
            for (int i = 0; i < bsc.readyUI.Length; ++i)
            {
                bsc.readyUI[i].SetActive(value);
            }
        }
      

        
    }
}