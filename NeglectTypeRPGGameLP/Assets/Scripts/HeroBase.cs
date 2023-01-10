using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroBase
{
    public HeroInfo heroInfo;
    public HeroStat heroStat;

    private float maxHealth;
    public float prevHealth;
    public float curHealth=100;
    private float maxGage;
    public float curSkillGage;

    SkillInfo[] skill;

    public int CompareTo(HeroBase x)
    {
        if (heroStat.Luck > x.heroStat.Luck)
            return 1;
        else if (heroStat.Luck == x.heroStat.Luck)
            return 0;
        else
            return -1;
    }

}
