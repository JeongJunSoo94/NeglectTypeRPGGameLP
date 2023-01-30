using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class AnimationNode : ActionNode
    {
        public string aniName;
        public bool runToTheEnd;
        HeroContext context;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            ChangeAnimationState(aniName);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (runToTheEnd)
            {
                if (context.animator.GetCurrentAnimatorStateInfo(0).IsName(aniName) &&context.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    return State.Success;
                return State.Running;
            }
            return State.Success;
        }

        void ChangeAnimationState(string newState)
        {
            if (context.currentAniState == newState) return;

            context.animator.StopPlayback();
            context.animator.Play(newState);
            context.currentAniState = newState;
        }

    }
}