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
        public bool cutAnimation;
        public List<string> aniNames;
        HeroContext context;
        string aniName;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            if (onRandom)
                aniName = aniNames[Random.Range(0, aniNames.Count)];
            else
                aniName = aniNames[0];
            if (aniName == "Idle")
                Debug.Log("Idle");
            else if (aniName == "hurt")
                Debug.Log("hurt");
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
                if (context.animator.GetCurrentAnimatorStateInfo(0).IsName(aniName) && context.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    return State.Success;
                return State.Running;
            }
            return State.Success;
        }
        void ChangeAnimation(string newState)
        {
            Debug.Log("바꾸기 실행");
            context.animator.StopPlayback();
            context.animator.Play(newState);
            context.currentAniState = newState;
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