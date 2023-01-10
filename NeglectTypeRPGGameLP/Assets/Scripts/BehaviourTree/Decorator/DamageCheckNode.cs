using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class DamageCheckNode : DecoratorNode
    {
        HeroContext HC;
        BattleSystemContext bsc;
        protected override void OnStart()
        {
            if (HC == null)
            {
                HC = blackBoard.context as HeroContext;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (HC.info.curHealth <= 0)
            {
                //юс╫ц
                if (blackBoard.data.TeamCheck(blackBoard)==Team.RED)
                    blackBoard.data.redCount--;
                else
                    blackBoard.data.blueCount--;
                
                HC.gameObject.SetActive(false);
            }
            if (HC.info.prevHealth != HC.info.curHealth)
            {
                HC.info.prevHealth = HC.info.curHealth;
            }
                //if (HC.info.prevHealth != HC.info.curHealth)
                //{
                //    child.Update();
                //    return State.Running;
                //}
           return State.Failure;
        }

        public BattleSystemContext GetBSC()
        {
            if (bsc == null)
            {
                HeroBlackBoard hb = blackBoard as HeroBlackBoard;
                bsc = hb.battleSystemBlackboard.context as BattleSystemContext;
            }
            return bsc;
        }
    }
}