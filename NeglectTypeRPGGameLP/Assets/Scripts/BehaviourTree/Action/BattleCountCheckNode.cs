using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleCountCheckNode : ActionNode
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
            if (BSC.state == BattleState.Battle)
            {
                if(BattlePossibleCheck())
                    return State.Success;
            }
            return State.Failure;
        }

        bool BattlePossibleCheck()
        {
            if (BSC.heroRedBattleList.Count().Equals(0))
            {
                BSC.winner = Win.BLUE;
                BSC.state = BattleState.End;
                return false;
            }
            if (BSC.heroBlueBattleList.Count().Equals(0))
            {
                BSC.winner = Win.RED;
                BSC.state = BattleState.End;
                return false;
            }
            return true;
        }
    }
}