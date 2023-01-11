using JJS.BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystemBlackboard : Blackboard
{
    public TabManager UI;
    public void BattleStart()
    {
        BattleSystemContext bsc = context as BattleSystemContext;
        bsc.isStart = true;
    }

}
