using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class DamageCheckNode : DecoratorNode
    {
        HeroContext HC;
        public int count;
        protected override void OnStart()
        {
            if (HC == null)
            {
                HC = blackBoard.context as HeroContext;
            }
            count = HC.info.damageCount;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (HC.info.damageCount>0&& HC.info.curHealth > 0)
            {
                return child.Update();
            }
            return State.Failure;
        }

    }
}