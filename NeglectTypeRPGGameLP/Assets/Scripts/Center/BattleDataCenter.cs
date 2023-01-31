using NeglectTypeRPG;
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

public enum Team
{
    None,
    RED,
    BLUE
}

public class BattleDataCenter : MonoBehaviour
{
    public List<GameObject> redCell = new List<GameObject>();
    public List<GameObject> blueCell = new List<GameObject>();

    public List<Blackboard> RedHero;
    public List<Blackboard> BlueHero = new List<Blackboard>();

    [HideInInspector] public PriorityQueue<HeroContext> heroRedBattleList = new PriorityQueue<HeroContext>();
    [HideInInspector] public PriorityQueue<HeroContext> heroBlueBattleList = new PriorityQueue<HeroContext>();

    public bool isRedTurn = true;

    public int redCount;
    public int blueCount;
    
    public Team winner;

    private void Awake()
    {
        RedHero = new List<Blackboard>(new Blackboard[5]);
    }

    public Team TeamCheck(Blackboard blackboard)
    {
        for (int i = 0; i < RedHero.Count; ++i)
        {
            if (RedHero[i] != null)
            { 
                if (RedHero[i].Equals(blackboard))
                    return Team.RED;
            }
        }
        for (int i = 0; i < BlueHero.Count; ++i)
        {
            if (BlueHero[i].Equals(blackboard))
                return Team.BLUE;
        }
        return Team.RED;
    }

    public void Initialized()
    {
        isRedTurn = true;
    }

}
