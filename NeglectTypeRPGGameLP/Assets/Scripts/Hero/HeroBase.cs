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

            if (!init)
            {
                if (heroInfo.normalAttack.action == 0)
                {
                    for (int i = 0; i < heroInfo.normalAttack.Stats.Count; ++i)
                    {
                        SkillDelegateInit(heroInfo.normalAttack, 0);
                    }

                }

                for (int i = 0; i < heroInfo.skills.Count; ++i)
                {
                    if (heroInfo.skills[i].action == 0)
                    {
                        for (int j = 0; j < heroInfo.skills[i].Stats.Count; ++j)
                        {
                            SkillDelegateInit(heroInfo.skills[i], i);
                        }
                    }
                }
                //    attack += new Attack(GaugeUp);
                //weaponDefence += new Defence(WeaponDefence);
                //weaponDefence += new Defence(WeaponDefenceMitigation);
                //tacticalDefence += new Defence(TacticalDefence);
                //tacticalDefence += new Defence(TacticalDefenceMitigation);
                //init = true;
            }
        }
        public void SkillDelegateInit(SkillInfo info, int index)
        {
            if (info.action == 0)
            {
                //StatAdd(info, index);
            }
            else
            { 
            }
        }

        public void StatAdd(SkillInfo stat, int index,int damageIndex,Attack attack)
        {
            switch (stat.Stats[index].skillDamageStats[damageIndex].statType)
            {
                case 0:          break;
                case 1:          break;
                case 2:          break;
                case 3:          break;
                case 4:          break;
                case 5:          break;
                case 6:          break;
                case 7:          break;
                case 8:          break;
                case 9:          break;
                case 10:         break;
                case 11:         break;
                case 12:         break;
                case 13:         break;
                case 14:         break;
                case 15:         break;
                case 16:         break;
                case 17:         break;
                case 18:         break;
                case 19:         break;
                case 20:         break;
                case 21:         break;
                case 22:         break;
                case 23:         break;
                case 24:         break;
                case 25:         break;
            }
        }

        public void PassiveAdd(SkillInfo info, int index)
        {
            //switch (info.damageType)
            //{
            //    case 0:
            //        passives[index] += new Passive(WeaponDamage);
            //        passives[index] += new Passive(WeaponDamageBonus);
            //        break;
            //    case 1:
            //        passives[index] += new Passive(TacticalDamage);
            //        passives[index] += new Passive(TacticalDamageBonus);
            //        break;
            //}
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

