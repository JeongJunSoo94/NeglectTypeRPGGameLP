using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class ResultUINode : ActionNode, ISystemNode
    {
        BattleSystemContext battleSystemContext;
        public string text;
        public bool isOn;
        protected override void OnStart()
        {
            if (battleSystemContext == null)
            {
                battleSystemContext = blackBoard.context as BattleSystemContext;
            }
            resultUI(text,isOn);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
        public void resultUI(string text, bool enable)
        {
            battleSystemContext.resultText.gameObject.SetActive(enable);
            if(enable)
                battleSystemContext.resultText.text = text;
        }


    }
}