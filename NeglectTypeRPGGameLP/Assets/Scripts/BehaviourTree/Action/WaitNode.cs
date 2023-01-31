using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class WaitNode : ActionNode
    {
        public WaitForSeconds wait;

        public bool delayEnable;
        public bool delayCheck;
        public float duration = 1;

        public bool onRandom;
        public float startTime; 
        public float maxTime;

        protected override void OnStart()
        {
            if(wait==null)
                wait = new WaitForSeconds(0.01f);
            if(onRandom)
                DelayCoroutine(startTime, maxTime);
            else
                DelayCoroutine(duration);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if(delayEnable)
                return State.Running;
            return State.Success;
        }

        public void DelayCoroutine(float delayTime)
        {
            delayEnable = true;
            blackBoard.StartCoroutine(Delay(delayTime));
        }

        public void DelayCoroutine(float startTime, float maxTime)
        {
            float time = Random.Range(startTime, maxTime + 1);
            delayEnable = true;
            blackBoard.StartCoroutine(Delay(time));
        }

        IEnumerator Delay(float delayTime)
        {
            float curCool = 0;
            while (curCool < delayTime)
            {
                curCool += 0.01f;
                yield return wait;
            }
            delayEnable = false;
            yield break;
        }
    }
}