using JJS.BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemContext : Context
{
    [HideInInspector] public BattleState state;

    public GameObject[] UI;
    public GameObject[] EndUI;

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
        state = BattleState.Start;
    }


}
