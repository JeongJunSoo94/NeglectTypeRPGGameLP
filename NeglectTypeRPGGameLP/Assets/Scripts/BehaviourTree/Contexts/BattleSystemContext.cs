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
    public override void InitContext()
    {
        isRedTurn = true;
        for (int i = 0; i < RedHero.Count; i++)
        {
            heroRedBattleList.Enqueue(RedHero[i].GetComponent<HeroContext>().GetInfo());
            RedHero[i].GetComponent<HeroContext>().isRed = true;
            RedHero[i].GetComponent<HeroContext>().bsc = this;
            //heroBattleList.Add(RedHero[i].heroStat.Luck, RedHero[i]);
        }

        for (int i = 0; i < BlueHero.Count; i++)
        {
            heroBlueBattleList.Enqueue(BlueHero[i].GetComponent<HeroContext>().GetInfo());
            BlueHero[i].GetComponent<HeroContext>().bsc = this;
            //heroBattleList.Add(RedHero[i].heroStat.Luck, RedHero[i]);
        }

        state = BattleState.Start;
    }

    

}
