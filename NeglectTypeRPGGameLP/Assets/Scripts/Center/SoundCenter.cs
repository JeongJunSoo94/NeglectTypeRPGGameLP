using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class SoundCenter : MonoBehaviour
    {
        public AudioClip[] bgm;
        public AudioClip[] sfx;

        public float bgmVolum = 1;
        public float sfxVolum = 1;
        public bool muteAll = false;

        public float curBGM;
        public float curSFX;
        public bool curMute;

        public ObjectPoolQueue Pool;

        private void Awake()
        {
            InfoInit();
            gameObject.AddComponent<AudioSource>();
            gameObject.GetComponent<AudioSource>().loop = true;
            SetBGM(0);
            BGMPlay();
            DontDestroyOnLoad(this.gameObject);
        }

        public void InfoInit()
        {
            curBGM = bgmVolum;
            curSFX = sfxVolum;
            curMute = muteAll;
        }

        public void InfoChange()
        {
            bgmVolum = curBGM;
            sfxVolum = curSFX;
            muteAll = curMute;
        }

        public void SetBGM(int index)
        {
            gameObject.GetComponent<AudioSource>().clip = bgm[index];
        }

        public void BGMPlay()
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

        public GameObject GetSFX(int index, Transform pos)
        {
            GameObject obj = Pool.Dequeue();
            obj.GetComponent<AudioSource>().clip = sfx[index];
            obj.GetComponent<AudioSource>().volume = sfxVolum;
            obj.transform.position = pos.position;
            obj.SetActive(true);
            return obj;
        }

        public void SetBGMVolum()
        {
            gameObject.GetComponent<AudioSource>().volume = curBGM;
        }

        public void SetSFXVolum()
        {
            AudioSource[] objList = GetComponentsInChildren<AudioSource>();
            AudioSource obj = GetComponent<AudioSource>();
            for (int i = 0; i < objList.Length; i++)
            {
                if (obj != objList[i])
                    objList[i].volume = curSFX;
            }
        }

        public void SetMute()
        {
            AudioSource[] objList = GetComponentsInChildren<AudioSource>();
            for (int i = 0; i < objList.Length; i++)
            {
                objList[i].mute = curMute;
            }
        }
    }

}
