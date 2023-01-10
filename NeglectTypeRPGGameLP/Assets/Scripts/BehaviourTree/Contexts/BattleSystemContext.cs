using JJS.BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState
{
    Ready,
    Start,
    Battle,
    BattleWaitTurn,
    BattleEndTurn,
    End
}

public enum Win
{
    RED,
    BLUE
}

public class BattleSystemContext : Context
{
    public List<GameObject> RedHero = new List<GameObject>();
    public List<GameObject> BlueHero = new List<GameObject>();

    [HideInInspector] public PriorityQueue<HeroContext> heroRedBattleList = new PriorityQueue<HeroContext>();
    [HideInInspector] public PriorityQueue<HeroContext> heroBlueBattleList = new PriorityQueue<HeroContext>();
    [HideInInspector] public BattleState state;
    public Win winner;
    public bool isRedTurn = true;

    public int redCount;
    public int blueCount;

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
