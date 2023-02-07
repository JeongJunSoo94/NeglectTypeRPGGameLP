using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public delegate void buff();
    public delegate float Skill(HeroBase stat,int index);
    public delegate void HeroUI();
    public delegate float Defence(HeroBase stat, int index);
    public delegate void Attack(HeroBase stat, int index);
    public delegate void Passive(HeroBase stat, int index);
    public delegate void Find(List<HeroBase> obj);
    public delegate void DamageOverTime();
    public class HeroBase
    {
        public HeroInfo heroInfo;
        //public HeroStat heroStat;

        public float maxHealth;
        public float prevHealth;
        public float curHealth;
        public float maxMana;
        public float curMana;

        public Defence weaponDefence;
        public Defence tacticalDefence;

        public List<Attack> attacks = new();
        public List<Passive> passives = new();

        public HeroUI heroUI;

        public float damage;
        public float defence;

        bool init = false;
        public bool isDamaged = false;
        public void Initialized()
        {
            maxHealth = heroInfo.Health_Point * heroInfo.Vital;
            curHealth = maxHealth;
            prevHealth = curHealth;
            maxMana = 100.0f;
            curMana = 0;
            isDamaged = false;
            heroUI();
           
            //if (!init)
            //{
            //    if (heroInfo.normalAttack.action == 0)
            //    {
            //        switch (heroInfo.normalAttack.damageType)
            //        {
            //            case 0:
            //                attack += new Attack(WeaponDamage);
            //                attack += new Attack(WeaponDamageBonus);
            //                break;
            //            case 1:
            //                attack += new Attack(TacticalDamage);
            //                attack += new Attack(TacticalDamageBonus);
            //                break;
            //        }
            //    }
            //    for (int i = 0; i < heroInfo.skills.Count; ++i)
            //    {
            //        if (heroInfo.skills[i].action == 0)
            //        {
            //            switch (heroInfo.skills[i].damageType)
            //            {
            //                case 0:
            //                    skills.Add(new Skill(WeaponDamage));
            //                    skills[skills.Count-1] += new Skill(WeaponDamageBonus);
            //                    break;
            //                case 1:
            //                    skills.Add(new Skill(TacticalDamage));
            //                    skills[skills.Count - 1] += new Skill(TacticalDamageBonus);

            //                    break;
            //            }
            //            skills[skills.Count - 1] += new Skill(GaugeDown);
            //        }
            //    }
            //    attack += new Attack(GaugeUp);
                //weaponDefence += new Defence(WeaponDefence);
                //weaponDefence += new Defence(WeaponDefenceMitigation);
                //tacticalDefence += new Defence(TacticalDefence);
                //tacticalDefence += new Defence(TacticalDefenceMitigation);
                //init = true;
            //}
        }

        public void SkillDelegateInit(SkillInfo info,int index)
        {
            if (info.action == 0)
            {
                AttackAdd(info, index);
            }
            else
            { 
            }
        }

        public void AttackAdd(SkillInfo info, int index)
        {
            switch (info.damageType)
            {
                case 0:
                    attacks[index] += new Attack(WeaponDamage);
                    attacks[index] += new Attack(WeaponDamageBonus);
                    break;
                case 1:
                    attacks[index] += new Attack(TacticalDamage);
                    attacks[index] += new Attack(TacticalDamageBonus);
                    break;
            }
        }

        public void PassiveAdd(SkillInfo info, int index)
        {
            switch (info.damageType)
            {
                case 0:
                    passives[index] += new Passive(WeaponDamage);
                    passives[index] += new Passive(WeaponDamageBonus);
                    break;
                case 1:
                    passives[index] += new Passive(TacticalDamage);
                    passives[index] += new Passive(TacticalDamageBonus);
                    break;
            }
        }

        public float GaugeUp(HeroBase hb, int index)
        {
            hb.curMana = 100;
            heroUI();
            return hb.damage;
        }

        public float GaugeDown(HeroBase hb, int index)
        {
            hb.curMana = 0;
            heroUI();
            return hb.damage;
        }

        //데미지에 무기,전술 두가지 종류의 데미지가 있다.
        //해당 타입의 공격이 오면 
        public void Damaged(HeroBase hb, int index)
        {

            //switch (hb.heroInfo.)
            //{
            //    case 0:
            //        damage = weaponDefence(this, index) - hb.heroInfo.Critical_Pierces_Defensive - hb.attack(hb, index);
            //        break;
            //    case 1:
            //        damage = tacticalDefence(this, index) - hb.heroInfo.Critical_Pierces_Defensive - hb.attack(hb, index);
            //        break;
            //}
            //switch (hb.heroInfo.skills[index].damageType)
            //{
            //    case 0:
            //        damage = weaponDefence(this, index) - hb.heroInfo.Critical_Pierces_Defensive - hb.skills[index](hb, index);
            //        break;
            //    case 1:
            //        damage = tacticalDefence(this, index) - hb.heroInfo.Critical_Pierces_Defensive - hb.skills[index](hb, index);
            //        break;
            //}
            if (damage < 0)
                curHealth += damage;
            isDamaged = true;
            heroUI();
        }

        public void WeaponDamage(HeroBase hb, int index)
        {
            hb.damage = (hb.heroInfo.Attack + hb.heroInfo.Strength) * hb.heroInfo.Weapon_Attack;
        }

        public void WeaponDamageBonus(HeroBase hb, int index)
        {
            hb.damage += (hb.heroInfo.Damage_Bonus + hb.heroInfo.Strength) * hb.heroInfo.Weapon_Damage_Bonus * 0.01f;
        }

        public void TacticalDamage(HeroBase hb, int index)
        {
            hb.damage = (hb.heroInfo.Attack + hb.heroInfo.Intelligence) * hb.heroInfo.Tactical_Attack;
        }

        public void TacticalDamageBonus(HeroBase hb, int index)
        {
            hb.damage += (hb.heroInfo.Damage_Bonus + hb.heroInfo.Intelligence) * hb.heroInfo.Tactical_Damage_Bonus * 0.01f;
        }

        public void WeaponDefence(HeroBase hb, int index)
        {
            defence = (hb.heroInfo.Defensive + hb.heroInfo.Agility) * hb.heroInfo.Weapon_Defensive;
        }

        public void WeaponDefenceMitigation(HeroBase hb, int index)
        {
            defence += (hb.heroInfo.Damage_Mitigation + hb.heroInfo.Agility) * hb.heroInfo.Weapon_Damage_Mitigation;
        }

        public void TacticalDefence(HeroBase hb, int index)
        {
            defence = (hb.heroInfo.Defensive + hb.heroInfo.Agility) * hb.heroInfo.Tactical_Defensive;
        }

        public void TacticalDefenceMitigation(HeroBase hb, int index)
        {
            defence += (hb.heroInfo.Damage_Mitigation + hb.heroInfo.Agility) * hb.heroInfo.Tactical_Damage_Mitigation;
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

