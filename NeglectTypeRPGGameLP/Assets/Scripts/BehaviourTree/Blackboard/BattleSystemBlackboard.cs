using JJS.BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystemBlackboard : Blackboard
{
    public TabManager UI;
    public void BattleStart()
    {
        for (int i = 0; i < data.RedHero.Count; ++i)
        {
            if (data.RedHero[i] != null)
                break;
            if (i == 4)
                return;
        }

        BattleSystemContext bsc = context as BattleSystemContext;
        bsc.isStart = true;
    }

}
