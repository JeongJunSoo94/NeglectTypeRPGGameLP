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
        public bool run;
        GameObject soundObj;
        protected override void OnStart()
        {
            if (heroBlackBoard == null)
            {
                context = blackBoard.context as HeroContext;
                heroBlackBoard = blackBoard as HeroBlackBoard;
            }
            if (soundObj == null)
            {
                //soundObj = heroBlackBoard.battleSystemBlackboard.effectcenter.GetSFX(audioIndex, blackBoard.transform);
            }
            else if (soundObj.activeSelf)
            {
                soundObj.GetComponent<SFX>().SoundRePeat();
            }
            else
            {
                //soundObj = heroBlackBoard.battleSystemBlackboard.effectcenter.GetSFX(audioIndex, blackBoard.transform);
            }
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
