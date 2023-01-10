using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class WaitNode : ActionNode
    {
        public WaitForSeconds wait;

        public bool delayEnable;
        public bool delayCheck;
        //BattleSystemContext BSC;
        public float duration = 1;
        protected override void OnStart()
        {
            wait = new WaitForSeconds(0.01f);
            //if (BSC == null)
            //{
            //    BSC = blackBoard.context as BattleSystemContext;
            //}
            //if (!BSC.delayEnable)
            //{
            //    BSC.DelayCoroutine(duration);
            //    BSC.delayCheck = false;
            //}
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            //if (!BSC.delayEnable)
            //{
            //    return State.Success;
            //}
            return State.Running;
        }

        public void DelayCoroutine(float delayTime)
        {
            delayEnable = true;
            blackBoard.BlackboardStartCoroutine(Delay(delayTime));
        }

        public void DelayCoroutine(float startTime, float maxTime)
        {
            float time = Random.Range(startTime, maxTime + 1);
            delayEnable = true;
            blackBoard.BlackboardStartCoroutine(Delay(time));
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