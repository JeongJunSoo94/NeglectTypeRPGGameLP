using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class SoundNode : ActionNode,ICharacterNode
    {
        HeroContext context;
        HeroBlackBoard heroBlackBoard;
        public int audioIndex;
        public bool run;
        protected override void OnStart()
        {
            if (heroBlackBoard == null)
            {
                context = blackBoard.context as HeroContext;
                heroBlackBoard = blackBoard as HeroBlackBoard;
            }
            heroBlackBoard.battleSystemBlackboard.soundCenter.GetSFX(audioIndex, blackBoard.transform);
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
