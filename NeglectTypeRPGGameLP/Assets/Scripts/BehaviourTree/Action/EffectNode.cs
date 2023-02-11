using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class EffectNode : ActionNode, ICharacterNode
    {
        HeroContext context;
        HeroBlackBoard heroBlackBoard;
        public int audioIndex;
        public bool onVFX;
        GameObject vfxObj;
        protected override void OnStart()
        {
            if (heroBlackBoard == null)
            {
                context = blackBoard.context as HeroContext;
                heroBlackBoard = blackBoard as HeroBlackBoard;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (context.VFX != null)
            {
                context.VFX.SetActive(onVFX);
            }
            //heroBlackBoard.battleSystemBlackboard.effectcenter.GetVFX(0,);
            return State.Success;
        }
    }
}
