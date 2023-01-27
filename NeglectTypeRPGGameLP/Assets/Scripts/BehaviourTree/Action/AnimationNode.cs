using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class AnimationNode : ActionNode
    {
        public string name;
        public bool runToTheEnd;
        HeroContext context;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            ChangeAnimationState(name);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (runToTheEnd)
            { 
                if (context.animator.GetCurrentAnimatorStateInfo(0).IsName(name) &&context.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
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