using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleTurnArrangementNode : ActionNode
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
            HeroBase hero = BSC.heroBattleList.Dequeue();


            return State.Success;
        }
    }
}