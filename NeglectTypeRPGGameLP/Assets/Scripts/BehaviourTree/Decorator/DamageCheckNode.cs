using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
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
            if (HC.info.prevHealth != HC.info.curHealth)
            {
                return child.Update();
            }
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