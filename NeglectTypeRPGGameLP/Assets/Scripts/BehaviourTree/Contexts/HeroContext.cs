using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NeglectTypeRPG;

public class HeroContext : Context, IComparable<HeroContext>
{
    public HeroBase info;

    public Slider hpBar;
    public Slider mpBar;

    public bool myTurn;
 
    public Vector3 originPos;
    public Vector3 targetPos;

    public List<GameObject> targets;

    public Animator animator;
    public string currentAniState;
    public HeroBase GetInfo()
    {
        if (info == null)
        {
            info = new HeroBase();
            originPos = transform.position;
            info.prevHealth = info.curHealth;
        }
        return info;
    }

    private void OnDisable()
    {
        myTurn = false;
    }

    public void Initialized()
    {
        originPos = transform.position;
        info.Initialized();
    }

    public override void InitContext()
    {
        if (info == null)
        { 
            info = new HeroBase();
            info.heroInfo = Instantiate(DataManager.Instance.heroInfo[0]);
            info.heroStat = Instantiate(DataManager.Instance.heroStat[0]);
        }
        info.heroUI += new HeroUI(HealthBarUpdate);
        info.heroUI += new HeroUI(ManaBarUpdate);
        originPos = transform.position;
        info.Initialized();
    }

    public void HealthBarUpdate()
    {
        hpBar.value = info.curHealth / info.maxHealth;
    }

    public void ManaBarUpdate()
    {
        mpBar.value = info.curMana / info.maxMana;
    }

    public int CompareTo(HeroContext x)
    {
        if (info.heroStat.Luck > x.info.heroStat.Luck)
            return 1;
        else if (info.heroStat.Luck == x.info.heroStat.Luck)
            return 0;
        else
            return -1;
    }
}
