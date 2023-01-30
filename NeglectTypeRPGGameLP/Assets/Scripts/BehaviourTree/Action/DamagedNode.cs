using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class DamagedNode : ActionNode, ICharacterNode
    {
        HeroContext HC;
        protected override void OnStart()
        {
            if (HC == null)
            {
                HC = blackBoard.context as HeroContext;
            }
            HC.info.prevHealth = HC.info.curHealth;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}