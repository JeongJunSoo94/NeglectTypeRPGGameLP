using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class AnimationNode : ActionNode
    {
        public bool onRandom;
        public bool runToTheEnd;
        public bool prevRunToTheEnd;
        public List<string> aniNames;
        HeroContext context;
        string aniName;
        bool prevEnd;
        bool check;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            prevEnd = prevRunToTheEnd;
            check = true;
            if (onRandom)
                aniName = aniNames[Random.Range(0, aniNames.Count)];
            else
                aniName = aniNames[0];
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (prevEnd)
            {
                if (context.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    prevEnd = false;
                }
                return State.Running;
            }
            else
            {
                if (check)
                {
                    ChangeAnimationState(aniName);
                    check = false;
                }
                if (runToTheEnd)
                {
                    if (context.animator.GetCurrentAnimatorStateInfo(0).IsName(aniName) &&context.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        return State.Success;
                    return State.Running;
                }
                return State.Success;
            }
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