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
            //if (!BSC.heroBattleList.Count().Equals(0))
            //{
            //    HeroBase hero = BSC.heroBattleList.Dequeue();
            //}

            if (BSC.isStart)
                return State.Success;
            return State.Running;
        }
    }
}