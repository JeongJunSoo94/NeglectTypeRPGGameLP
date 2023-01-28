using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class SFX : MonoBehaviour
    {
        [SerializeField]
        AudioSource sound;
        private void OnEnable()
        {
            if (GetComponent<AudioSource>().clip != null)
                SoundPlay();
        }

        public void SoundPlay()
        {
            StartCoroutine("SoundCoroutine");
        }

        IEnumerator SoundCoroutine()
        {
            sound.Play();
            float delayTime = sound.clip.length;
            yield return new WaitForSeconds(delayTime);
            this.gameObject.SetActive(false);
        }
    }

}
