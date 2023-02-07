using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class AnimationNode : ActionNode
    {
        public bool onRandom;
        [Range(0, 1)]
        public float runTime;
        [Range(0, 1)]
        public float startTime;
        [Range(0, 1)]
        public float endTime;

        public bool runToTheEnd;

        public bool prevRunToTheEnd;

        public bool cutAnimation;

        public int layer=0;
        public List<string> aniNames;
        HeroContext context;
        public string aniName;



        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
                SelectAnimation();
            }
            if (!AnimationCheck())
            {
                SelectAnimation();
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (cutAnimation)
                ChangeAnimation(aniName);
            else
                ChangeAnimationState(aniName);
            if (runToTheEnd)
            {
                if (context.animator.GetCurrentAnimatorStateInfo(0).IsName(aniName) && context.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= endTime)
                {
                    return State.Success;
                }
                return State.Running;
            }
            return State.Success;
        }
        void ChangeAnimation(string newState)
        {
            context.animator.StopPlayback();
            context.animator.Play(newState, layer, startTime);
            context.currentAniState = newState;
        }
        void ChangeAnimationState(string newState)
        {
            if (context.currentAniState == newState) return;

            context.animator.StopPlayback();
            context.animator.Play(newState, layer, startTime);
            context.currentAniState = newState;
        }

        void SelectAnimation()
        {
            if (onRandom)
                aniName = aniNames[Random.Range(0, aniNames.Count)];
            else
                aniName = aniNames[0];
        }
        bool AnimationCheck()
        {
            for (int i = 0; i < aniNames.Count; ++i)
            {
                if(context.animator.GetCurrentAnimatorStateInfo(0).IsName(aniNames[i]))
                    return true;
            }
            return false;
        }
    }
}