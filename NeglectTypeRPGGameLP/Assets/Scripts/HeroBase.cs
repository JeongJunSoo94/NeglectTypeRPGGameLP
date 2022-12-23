using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroBase : MonoBehaviour
{
    HeroInfo heroInfo;
    HeroStat heroStat;
    SkillInfo[] skill;

    public HeroBase(HeroInfo heroInfo, HeroStat heroStat, SkillInfo[] skill)
    {
        this.heroInfo = heroInfo;
        this.heroStat = heroStat;
        this.skill = skill;
    }
}
