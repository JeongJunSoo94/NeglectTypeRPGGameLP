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

public enum Team
{
    None,
    RED,
    BLUE
}

public class BattleDataCenter : MonoBehaviour
{
    public List<Blackboard> RedHero = new List<Blackboard>();
    public List<Blackboard> BlueHero = new List<Blackboard>();

    [HideInInspector] public PriorityQueue<HeroContext> heroRedBattleList = new PriorityQueue<HeroContext>();
    [HideInInspector] public PriorityQueue<HeroContext> heroBlueBattleList = new PriorityQueue<HeroContext>();

    public bool isRedTurn = true;

    public int redCount;
    public int blueCount;
    
    public Team winner;

    public Team TeamCheck(Blackboard blackboard)
    {
        if (RedHero.Equals(blackboard))
            return Team.RED;
        if (BlueHero.Equals(blackboard))
            return Team.BLUE;
        return Team.RED;
    }
}
