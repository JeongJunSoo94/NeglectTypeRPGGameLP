using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroBase : MonoBehaviour
{
    public HeroInfo heroInfo;
    public HeroStat heroStat;



    private float maxHealth;
    public float curHealth;
    private float maxGage;
    public float curSkillGage;

    SkillInfo[] skill;

    public void print()
    {
        Debug.Log(heroInfo.name);
    }
}
