using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class BattleUINode : ActionNode, ISystemNode
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
            //BattleUI(false);
            return State.Success;
        }

        //public void BattleUI(bool use)
        //{
        //    for (int i = 0; i < BSC.UI.Length; i++)
        //    {
        //        BSC.UI[i].SetActive(use);
        //    }
        //}
    }
}