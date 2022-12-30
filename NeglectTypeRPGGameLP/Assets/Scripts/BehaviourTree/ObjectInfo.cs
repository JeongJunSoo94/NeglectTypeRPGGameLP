using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class ObjectInfo : MonoBehaviour
    {
        GameObject prefabObject;
        public Animator animator;
        public string currentAniState;
        public int syncIndex;
        public bool localSync;
        public bool delayEnable;
        public bool delayCheck;
        public WaitForSeconds wait;
        private void Awake()
        {
            prefabObject = gameObject;
            syncIndex =0;
            delayEnable = false;
            delayCheck = true;
            localSync = true;
            animator = GetComponent<Animator>();
            currentAniState = "";
            wait = new WaitForSeconds(0.01f);
        }
        public GameObject PrefabObject
        {
            get
            {
                return prefabObject;
            }
            set
            {
                prefabObject = value;
            }
        }

        public void DelayCoroutine(float delayTime)
        {
            delayEnable = true;
            StartCoroutine(Delay(delayTime));
        }

        public void DelayCoroutine(float startTime, float maxTime)
        {
            float time = Random.Range(startTime, maxTime+1);
            delayEnable = true;
            StartCoroutine(Delay(time));
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
