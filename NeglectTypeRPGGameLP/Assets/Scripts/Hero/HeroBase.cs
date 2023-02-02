using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public delegate void buff();
    public delegate float Skill(HeroBase stat);
    public delegate void HeroUI();
    public delegate float Defence(HeroBase stat);
    public delegate float Attack(HeroBase stat);
    public delegate void Find(List<HeroBase> obj);
    public class HeroBase
    {
        public HeroInfo heroInfo;
        //public HeroStat heroStat;

        public float maxHealth;
        public float prevHealth;
        public float curHealth;
        public float maxMana;
        public float curMana;

        public Attack attack;

        public Defence weaponDefence;
        public Defence tacticalDefence;

        public Skill skill;

        public HeroUI heroUI;

        public float damage;
        public float damaged;
        public float defence;

        bool init = false;
        public int damageCount = 0;
        public void Initialized()
        {
            maxHealth = heroInfo.Health_Point * heroInfo.Vital;
            curHealth = maxHealth;
            prevHealth = curHealth;
            maxMana = 100.0f;
            curMana = 0;
            damageCount = 0;
            heroUI();
            if (!init)
            {
                switch (heroInfo.DamageType)
                {
                    case 0:
                        attack += new Attack(WeaponDamage);
                        attack += new Attack(WeaponDamageBonus);
                        skill += new Skill(WeaponDamage);
                        skill += new Skill(WeaponDamageBonus);
                        break;
                    case 1:
                        attack += new Attack(TacticalDamage);
                        attack += new Attack(TacticalDamageBonus);
                        skill += new Skill(TacticalDamage);
                        skill += new Skill(TacticalDamageBonus);
                        break;
                }
                attack += new Attack(GaugeUp);
                weaponDefence += new Defence(WeaponDefence);
                weaponDefence += new Defence(WeaponDefenceMitigation);
                tacticalDefence += new Defence(TacticalDefence);
                tacticalDefence += new Defence(TacticalDefenceMitigation);
                skill += new Skill(GaugeDown);
                init = true;
            }
        }

        public float GaugeUp(HeroBase hb)
        {
            hb.curMana = 100;
            heroUI();
            return hb.damage;
        }

        public float GaugeDown(HeroBase hb)
        {
            hb.curMana = 0;
            heroUI();
            return hb.damage;
        }

        //데미지에 무기,전술 두가지 종류의 데미지가 있다.
        //해당 타입의 공격이 오면 
        public void Damaged(HeroBase hb)
        {
            switch (hb.heroInfo.DamageType)
            {
                case 0:
                    damaged = weaponDefence(this) - hb.heroInfo.Critical_Pierces_Defensive - hb.attack(hb);
                    break;
                case 1:
                    damaged = tacticalDefence(this) - hb.heroInfo.Critical_Pierces_Defensive - hb.attack(hb);
                    break;
            }
            if (damaged < 0)
                curHealth += damaged;
            ++damageCount;
            heroUI();
        }

        public void SkillDamaged(HeroBase hb)
        {
            switch (hb.heroInfo.DamageType)
            {
                case 0:
                    damaged = weaponDefence(this) - hb.heroInfo.Critical_Pierces_Defensive - hb.skill(hb);
                    break;
                case 1:
                    damaged = tacticalDefence(this) - hb.heroInfo.Critical_Pierces_Defensive - hb.skill(hb);
                    break;
            }
            if (damaged < 0)
                curHealth += damaged;
            ++damageCount;
            heroUI();
        }

        public float WeaponDamage(HeroBase hb)
        {
            hb.damage = (hb.heroInfo.Attack + hb.heroInfo.Strength) * hb.heroInfo.Weapon_Attack;
            return hb.damage;
        }

        public float WeaponDamageBonus(HeroBase hb)
        {
            hb.damage += (hb.heroInfo.Damage_Bonus + hb.heroInfo.Strength) * hb.heroInfo.Weapon_Damage_Bonus * 0.01f;
            return hb.damage;
        }

        public float TacticalDamage(HeroBase hb)
        {
            hb.damage = (hb.heroInfo.Attack + hb.heroInfo.Intelligence) * hb.heroInfo.Tactical_Attack;
            return hb.damage;
        }

        public float TacticalDamageBonus(HeroBase hb)
        {
            hb.damage += (hb.heroInfo.Damage_Bonus + hb.heroInfo.Intelligence) * hb.heroInfo.Tactical_Damage_Bonus * 0.01f;
            return hb.damage;
        }

        public float WeaponDefence(HeroBase hb)
        {
            defence = (hb.heroInfo.Defensive + hb.heroInfo.Agility) * hb.heroInfo.Weapon_Defensive;
            return defence;
        }

        public float WeaponDefenceMitigation(HeroBase hb)
        {
            defence += (hb.heroInfo.Damage_Mitigation + hb.heroInfo.Agility) * hb.heroInfo.Weapon_Damage_Mitigation;
            return defence;
        }

        public float TacticalDefence(HeroBase hb)
        {
            defence = (hb.heroInfo.Defensive + hb.heroInfo.Agility) * hb.heroInfo.Tactical_Defensive;
            return defence;
        }

        public float TacticalDefenceMitigation(HeroBase hb)
        {
            defence += (hb.heroInfo.Damage_Mitigation + hb.heroInfo.Agility) * hb.heroInfo.Tactical_Damage_Mitigation;
            return defence;
        }



        public int CompareTo(HeroBase x)
        {
            if (heroInfo.Luck > x.heroInfo.Luck)
                return 1;
            else if (heroInfo.Luck == x.heroInfo.Luck)
                return 0;
            else
                return -1;
        }

    }

}

