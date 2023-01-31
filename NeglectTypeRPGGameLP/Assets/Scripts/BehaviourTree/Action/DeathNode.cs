using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class DeathNode : ActionNode, ICharacterNode
    {
        HeroContext HC;
        protected override void OnStart()
        {
            if (HC == null)
            {
                HC = blackBoard.context as HeroContext;
            }
        }

        protected override void OnStop()
        {
            if (HC.info.curHealth <= 0)
            {
                //юс╫ц
                if (blackBoard.data.TeamCheck(blackBoard) == Team.RED)
                    blackBoard.data.redCount--;
                else
                    blackBoard.data.blueCount--;

                HC.myTurn = false;
                HC.gameObject.SetActive(false);
            }
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

    }
}
