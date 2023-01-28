using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JJS.BT;

namespace NeglectTypeRPG
{
    public class DeathNode : ActionNode
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
            HC.info.prevHealth = HC.info.curHealth;

            if (HC.info.curHealth <= 0)
            {
                //юс╫ц
                if (blackBoard.data.TeamCheck(blackBoard) == Team.RED)
                    blackBoard.data.redCount--;
                else
                    blackBoard.data.blueCount--;

                HC.gameObject.SetActive(false);
            }
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

    }
}
