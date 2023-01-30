using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class BattleSystemContext : Context
    {
        public bool isStart;

        [HideInInspector] public BattleState state;

        public GameObject[] startUI;
        public GameObject[] readyUI;
        public GameObject[] defaultUI;
        public GameObject[] endUI;

        //public void LoseUI()
        //{
        //    EndUI[1].SetActive(true);
        //}

        //public void BattleUI(bool use)
        //{
        //    for (int i = 0; i < UI.Length; i++)
        //    {
        //        UI[i].SetActive(use);
        //    }
        //}

        //public void OnStart()
        //{
        //    isStart = true;
        //}

        public override void InitContext()
        {
            isStart = false;
            state = BattleState.Ready;
        }


    }

}
