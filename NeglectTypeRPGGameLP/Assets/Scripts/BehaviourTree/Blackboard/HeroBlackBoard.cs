using JJS.BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBlackBoard : Blackboard
{
    public BattleSystemBlackboard battleSystemBlackboard;
    public Vector3 moveToPosition;

    public override void Awake()
    {
        base.Awake();
    }
}
