using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContext : Context
{
    public HeroBase info;

    public BattleSystemContext bsc;

    //юс╫ц
    public HeroInfo heroInfo;
    public HeroStat heroStat;

    public bool win;

    public bool isRed;

    public Vector3 originPos;

    public GameObject target;

    public HeroBase Gets()
    {
        if(info ==null)
            info = gameObject.GetComponent<HeroBase>();

        return info;
    }

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
}
