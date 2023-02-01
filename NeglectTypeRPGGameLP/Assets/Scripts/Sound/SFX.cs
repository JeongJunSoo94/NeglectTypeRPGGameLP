using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class SFX : MonoBehaviour
    {
        [SerializeField]
        AudioSource sound;
        float delayTime;
        WaitForSeconds wait;
        float curTime;
        private void OnEnable()
        {
            if (wait == null)
                wait = new WaitForSeconds(0.01f);
            if (GetComponent<AudioSource>().clip != null)
                SoundPlay();
            
        }

        public void SoundPlay()
        {
            delayTime = sound.clip.length;
            StartCoroutine(SoundCoroutine());
        }

        public void SoundRePeat()
        {
            curTime = 0;
            sound.time = 0;
            sound.Play();
        }

        IEnumerator SoundCoroutine()
        {
            curTime = 0;
            sound.Play();
            float delayTime = sound.clip.length;
            while (curTime < delayTime)
            {
                curTime += 0.01f;
                yield return wait;
            }
            this.gameObject.SetActive(false);
        }
    }

}
