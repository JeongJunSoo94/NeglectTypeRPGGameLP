using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleTurnBehaviorNode : ActionNode
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
            HeroBase hero;
            for (int i = 0; i < BSC.heroBattleList.Count(); i++)
            {
                hero = BSC.heroBattleList.Dequeue();
                hero.myTurn = true;
            }


            return State.Success;
        }
    }
}