using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState
{
    Ready,
    Start,
    Battle,
    End
}

public class BattleSystemContext : Context
{

     public List<HeroBase> RedHero;
     public List<HeroBase> BlueHero;

    [HideInInspector] public PriorityQueue<HeroBase> heroBattleList = new PriorityQueue<HeroBase>();
    [HideInInspector] public BattleState state;

    public override void InitContext()
    {

        //RedHero = new List<HeroBase>();
        //BlueHero = new List<HeroBase>();
        for (int i = 0; i < RedHero.Count; i++)
        {
            heroBattleList.Enqueue(RedHero[i]);
            //heroBattleList.Add(RedHero[i].heroStat.Luck, RedHero[i]);
        }


        state = BattleState.Start;
    }

}
