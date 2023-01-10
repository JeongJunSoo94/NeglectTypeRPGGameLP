using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContext : Context, IComparable<HeroContext>
{
    public HeroBase info;

    //юс╫ц
    public HeroInfo heroInfo;
    public HeroStat heroStat;

    public bool myTurn;

    public bool win;

    public bool isRed;

    public Vector3 originPos;

    public GameObject target;

    public HeroBase GetInfo()
    {
        if (info == null)
        {
            info = new HeroBase();
            originPos = transform.position;
            info.prevHealth = info.curHealth;
            info.heroInfo = heroInfo;
            info.heroStat = heroStat;
        }
        return info;
    }

    public override void InitContext()
    {
        if (info == null)
        { 
            info = new HeroBase();
            originPos = transform.position;
        }
        info.prevHealth = info.curHealth;
        info.heroInfo = heroInfo;
        info.heroStat = heroStat;
    }

    public int CompareTo(HeroContext x)
    {
        if (info.heroStat.Luck > x.heroStat.Luck)
            return 1;
        else if (info.heroStat.Luck == x.heroStat.Luck)
            return 0;
        else
            return -1;
    }
}
