using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void buff();
public delegate void Skill();
public delegate void HealthUI();
public delegate float Defence(HeroBase stat);
public delegate float Attack(HeroBase stat);
public delegate void Find(List<HeroBase> obj);

public enum DamageType
{
    Weapon,
    Tactical
}
public class HeroBase
{
    public HeroInfo heroInfo;
    public HeroStat heroStat;

    public float maxHealth;
    public float prevHealth;
    public float curHealth;
    public float maxGage;
    public float curSkillGage;

    public Attack attack;

    public Defence weaponDefence;
    public Defence tacticalDefence;

    public Skill skill;

    public HealthUI healthBar;

    public float damage;
    public float damaged;
    public float defence;

    SkillInfo[] skills;

    bool init = false;

    public void Initialized()
    {
        maxHealth = heroStat.Health_Point * heroStat.Vital;
        curHealth = maxHealth;
        prevHealth = curHealth;
        maxGage = 100.0f;
        curSkillGage = 0;
        healthBar();
        if (!init)
        {
            switch (heroStat.DamageType)
            {
                case 0:
                    attack += new Attack(WeaponDamage);
                    attack += new Attack(WeaponDamageBonus);
                    break;
                case 1:
                    attack += new Attack(TacticalDamage);
                    attack += new Attack(TacticalDamageBonus);
                    break;
            }
            attack += new Attack(GageUp);
            weaponDefence += new Defence(WeaponDefence);
            weaponDefence += new Defence(WeaponDefenceMitigation);
            tacticalDefence += new Defence(TacticalDefence);
            tacticalDefence += new Defence(TacticalDefenceMitigation);
            init = true;
        }
    }

    public float GageUp(HeroBase hb)
    {
        hb.curSkillGage = 100;
        return 0;
    }

    //데미지에 무기,전술 두가지 종류의 데미지가 있다.
    //해당 타입의 공격이 오면 
    public void Damaged(HeroBase hb)
    {
        switch (hb.heroStat.DamageType)
        {
            case 0:
                damaged = weaponDefence(this) - hb.heroStat.Critical_Pierces_Defensive - hb.attack(hb);
                break;
            case 1:
                damaged = tacticalDefence(this)- hb.heroStat.Critical_Pierces_Defensive - hb.attack(hb);
                break;
        }
        if (damaged < 0)
            curHealth += damaged;
        healthBar();
    }

    public float WeaponDamage(HeroBase hb)
    {
        hb.damage = (hb.heroStat.Attack + hb.heroStat.Strength) * hb.heroStat.Weapon_Attack;
        return hb.damage;
    }

    public float WeaponDamageBonus(HeroBase hb)
    {
        hb.damage += (hb.heroStat.Damage_Bonus + hb.heroStat.Strength) * hb.heroStat.Weapon_Damage_Bonus*0.01f;
        return hb.damage;
    }

    public float TacticalDamage(HeroBase hb)
    {
        hb.damage = (hb.heroStat.Attack + hb.heroStat.Intelligence) * hb.heroStat.Tactical_Attack;
        return hb.damage;
    }

    public float TacticalDamageBonus(HeroBase hb)
    {
        hb.damage += (hb.heroStat.Damage_Bonus + hb.heroStat.Intelligence) * hb.heroStat.Tactical_Damage_Bonus * 0.01f;
        return hb.damage;
    }

    public float WeaponDefence(HeroBase hb)
    {
        defence = (hb.heroStat.Defensive + hb.heroStat.Agility) * hb.heroStat.Weapon_Defensive;
        return defence;
    }

    public float WeaponDefenceMitigation(HeroBase hb)
    {
        defence += (hb.heroStat.Damage_Mitigation + hb.heroStat.Agility) * hb.heroStat.Weapon_Damage_Mitigation;
        return defence;
    }

    public float TacticalDefence(HeroBase hb)
    {
        defence = (hb.heroStat.Defensive + hb.heroStat.Agility) * hb.heroStat.Tactical_Defensive;
        return defence;
    }

    public float TacticalDefenceMitigation(HeroBase hb)
    {
        defence += (hb.heroStat.Damage_Mitigation + hb.heroStat.Agility) * hb.heroStat.Tactical_Damage_Mitigation;
        return defence;
    }



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
