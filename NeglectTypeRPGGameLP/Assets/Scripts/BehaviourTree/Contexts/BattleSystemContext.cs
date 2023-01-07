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

    [HideInInspector] public PriorityQueue<HeroBase> heroRedBattleList = new PriorityQueue<HeroBase>();
    [HideInInspector] public PriorityQueue<HeroBase> heroBlueBattleList = new PriorityQueue<HeroBase>();
    [HideInInspector] public BattleState state;
    public Win winner;
    public bool isRedTurn = true;

    public int redCount;
    public int blueCount;

    public bool isStart;

    public GameObject[] UI;
    public GameObject[] EndUI;

    public void VictoryUI()
    {
        EndUI[0].SetActive(true);
    }

    public void LoseUI()
    {
        EndUI[1].SetActive(true);
    }

    public void BattleUI(bool use)
    {
        for (int i = 0; i < UI.Length; i++)
        {
            UI[i].SetActive(use);
        }
    }

    public void OnStart()
    {
        isStart = true;
    }

    public override void InitContext()
    {
        isRedTurn = true;
        redCount = RedHero.Count;
        blueCount = BlueHero.Count;
        for (int i = 0; i < redCount; i++)
        {
            heroRedBattleList.Enqueue(RedHero[i].GetComponent<HeroContext>().GetInfo());
            RedHero[i].GetComponent<HeroContext>().isRed = true;
            RedHero[i].GetComponent<HeroContext>().bsc = this;
            //heroBattleList.Add(RedHero[i].heroStat.Luck, RedHero[i]);
        }
        for (int i = 0; i < blueCount; i++)
        {
            heroBlueBattleList.Enqueue(BlueHero[i].GetComponent<HeroContext>().GetInfo());
            BlueHero[i].GetComponent<HeroContext>().bsc = this;
            //heroBattleList.Add(RedHero[i].heroStat.Luck, RedHero[i]);
        }
        state = BattleState.Start;
    }

}
