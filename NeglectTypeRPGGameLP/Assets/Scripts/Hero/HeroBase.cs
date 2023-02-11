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
    public delegate void Attack(HeroBase stat, int skillIndex);
    public delegate void SkillDel(ref Stat stat, Stat operatorStat);
    public delegate void Passive(HeroBase stat, int index,ref int turn);
    public delegate void Find(List<HeroBase> obj);
    public delegate void DamageOverTime();

    //public delegate Stat StatOperator(Stat stat);
    public class HeroBase
    {
        public HeroInfo heroInfo;
        public Stat curStat;
        SkillsData skillData;
        //public HeroStat heroStat;

        public float maxHealth;
        public float prevHealth;
        public float curHealth;
        public float maxMana;
        public float curMana;

        public Defence weaponDefence;
        public Defence tacticalDefence;

        //데미지와 관련된 연산만 넣으면 될듯하다.
        public List<SkillDel> actives = new();

        // 디버프도 어떻게 보면 패시브와 같다 그러니 디버프를 패시브에 넣어주면 될듯하다.
        // 몇턴동안 넣어주는지는 턴을 지속적으로 받아서 확인 패시브 횟수만큼 돌려줄거니깐 상관없음
        public List<SkillDel> passives = new();

        public HeroUI heroUI;

        public float damage;
        public float defence;

        bool init = false;
        public bool isDamaged = false;
        public void Initialized()
        {
            curStat = heroInfo.defaultStat;
            heroInfo.CombatPowerInit();
            maxHealth = curStat.Health_Point * curStat.Vital;
            curHealth = maxHealth;
            prevHealth = curHealth;
            maxMana = 100.0f;
            curMana = 0;
            isDamaged = false;
            heroUI();
            skillData = DataManager.Instance.skillData;
            if (!init)
            {
                for (int i = 0; i < heroInfo.normalAttack.Stats.Count; ++i)
                {
                    SkillDelegateInit(heroInfo.normalAttack, 0, heroInfo.level);
                }

                for (int i = 0; i < heroInfo.skills.Count; ++i)
                {
                    SkillDelegateInit(heroInfo.skills[i], i, heroInfo.level);
                }
                //    attack += new Attack(GaugeUp);
                //weaponDefence += new Defence(WeaponDefence);
                //weaponDefence += new Defence(WeaponDefenceMitigation);
                //tacticalDefence += new Defence(TacticalDefence);
                //tacticalDefence += new Defence(TacticalDefenceMitigation);
                //init = true;

            }
        }
        public void SkillDelegateInit(SkillInfo info, int index,int level)
        {
            //0 액티브 1 패시브
            //스킬의 액티브는 사용할때만 스킬의 패시브는 지속적
            //액티브라고 상대를 때리는 것은 아니다
            //단지 넣어서 지속적으로 값이 변동이 되어있나 액티브로 잠시동안만 변경이 되나의 차이다.
            //델리게이트 스킬 생성기
            if (info.action == 0)
            {
                actives.Add(new SkillDel(None));
                for (int i = 0; i < skillData.statDamageCache[info.ID - 1].skillStatDatas.Count; ++i)
                {
                    for (int j = 0; j < skillData.statDamageCache[info.ID - 1].skillStatDatas[i].skillDamageStats.Count; ++j)
                    {
                        SkillDamageStats skillDamageStat = skillData.statDamageCache[info.ID - 1].skillStatDatas[i].skillDamageStats[j];
                        switch (skillDamageStat.statType)
                        {
                            case 1:  actives[actives.Count - 1] += new SkillDel(Strength); break;
                            case 2:  actives[actives.Count - 1] += new SkillDel(Intelligence); break;
                            case 3:  actives[actives.Count - 1] += new SkillDel(Agility); break;
                            case 4:  actives[actives.Count - 1] += new SkillDel(Vital); break;
                            case 5:  actives[actives.Count - 1] += new SkillDel(Luck); break;
                            case 6:  actives[actives.Count - 1] += new SkillDel(CriticalImmunityRate); break;
                            case 7:  actives[actives.Count - 1] += new SkillDel(CriticalDamage); break;
                            case 8:  actives[actives.Count - 1] += new SkillDel(HealthPoint); break;
                            case 9:  actives[actives.Count - 1] += new SkillDel(Damage); break;
                            case 10: actives[actives.Count - 1] += new SkillDel(WeaponDamage); break;
                            case 11: actives[actives.Count - 1] += new SkillDel(TacticalDamage); break;
                            case 12: actives[actives.Count - 1] += new SkillDel(Defensive); break;
                            case 13: actives[actives.Count - 1] += new SkillDel(WeaponDefensive); break;
                            case 14: actives[actives.Count - 1] += new SkillDel(TacticalDefensive); break;
                            case 15: actives[actives.Count - 1] += new SkillDel(DamageBonus); break;
                            case 16: actives[actives.Count - 1] += new SkillDel(WeaponDamageBonus); break;
                            case 17: actives[actives.Count - 1] += new SkillDel(TacticalDamageBonus); break;
                            case 18: actives[actives.Count - 1] += new SkillDel(DamageMitigation); break;
                            case 19: actives[actives.Count - 1] += new SkillDel(WeaponDamageMitigation); break;
                            case 20: actives[actives.Count - 1] += new SkillDel(TacticalDamageMitigation); break;
                            case 21: actives[actives.Count - 1] += new SkillDel(CriticalPiercesDefensive); break;
                            case 22: actives[actives.Count - 1] += new SkillDel(FocusDamage); break;
                            case 23: actives[actives.Count - 1] += new SkillDel(ShieldBonusDamage); break;
                            case 24: actives[actives.Count - 1] += new SkillDel(DamageReflection); break;
                            case 25: actives[actives.Count - 1] += new SkillDel(ExtraHealingEffect); break;
                        }
                    }
                }
            }
            else
            {
                passives.Add(new SkillDel(None));
                for (int i = 0; i < skillData.statDamageCache[info.ID-1].skillStatDatas.Count; ++i)
                {
                    for (int j = 0; j < skillData.statDamageCache[info.ID-1].skillStatDatas[i].skillDamageStats.Count; ++j)
                    {
                        SkillDamageStats skillDamageStat = skillData.statDamageCache[info.ID-1].skillStatDatas[i].skillDamageStats[j];
                        switch (skillDamageStat.statType)
                        {
                            case 1: passives[passives.Count - 1] += new SkillDel(Strength); break;
                            case 2: passives[passives.Count - 1] += new SkillDel(Intelligence); break;
                            case 3: passives[passives.Count - 1] += new SkillDel(Agility); break;
                            case 4: passives[passives.Count - 1] += new SkillDel(Vital); break;
                            case 5: passives[passives.Count - 1] += new SkillDel(Luck); break;
                            case 6: passives[passives.Count - 1] += new SkillDel(CriticalImmunityRate); break;
                            case 7: passives[passives.Count - 1] += new SkillDel(CriticalDamage); break;
                            case 8: passives[passives.Count - 1] += new SkillDel(HealthPoint); break;
                            case 9: passives[passives.Count - 1] += new SkillDel(Damage); break;
                            case 10: passives[passives.Count - 1] += new SkillDel(WeaponDamage); break;
                            case 11: passives[passives.Count - 1] += new SkillDel(TacticalDamage); break;
                            case 12: passives[passives.Count - 1] += new SkillDel(Defensive); break;
                            case 13: passives[passives.Count - 1] += new SkillDel(WeaponDefensive); break;
                            case 14: passives[passives.Count - 1] += new SkillDel(TacticalDefensive); break;
                            case 15: passives[passives.Count - 1] += new SkillDel(DamageBonus); break;
                            case 16: passives[passives.Count - 1] += new SkillDel(WeaponDamageBonus); break;
                            case 17: passives[passives.Count - 1] += new SkillDel(TacticalDamageBonus); break;
                            case 18: passives[passives.Count - 1] += new SkillDel(DamageMitigation); break;
                            case 19: passives[passives.Count - 1] += new SkillDel(WeaponDamageMitigation); break;
                            case 20: passives[passives.Count - 1] += new SkillDel(TacticalDamageMitigation); break;
                            case 21: passives[passives.Count - 1] += new SkillDel(CriticalPiercesDefensive); break;
                            case 22: passives[passives.Count - 1] += new SkillDel(FocusDamage); break;
                            case 23: passives[passives.Count - 1] += new SkillDel(ShieldBonusDamage); break;
                            case 24: passives[passives.Count - 1] += new SkillDel(DamageReflection); break;
                            case 25: passives[passives.Count - 1] += new SkillDel(ExtraHealingEffect); break;
                        }
                    }
                    passives[passives.Count - 1](ref curStat, skillData.statCache[info.ID - 1].statDatas[info.Stats[i].damageID-1].stats[level - 1]);
                }
                
            }
        }


        public void GaugeUp(HeroBase hb, int index)
        {
            hb.curMana = 100;
            heroUI();
        }

        public float GaugeDown(HeroBase hb, int index)
        {
            hb.curMana = 0;
            heroUI();
            return hb.damage;
        }

        public void Damaged(HeroBase hb, int index,bool isSkill,int kind )
        {
            //hb.actives[index](hb, skillIndex);
            if (isSkill)
            {
                switch (kind)
                {
                    case 0:
                        damage = -hb.curStat.Damage;//weaponDefence(this, index) - hb.curStat.Critical_Pierces_Defensive - hb.attack(hb, index);
                        break;
                    case 1:
                        damage = -hb.curStat.Weapon_Damage;//tacticalDefence(this, index) - hb.curStat.Critical_Pierces_Defensive - hb.attack(hb, index);
                        break;
                    case 2:
                        damage = -hb.curStat.Tactical_Damage;
                        break;
                }
            }
            else 
            {
                switch (kind)
                {
                    case 0:
                        damage = -hb.curStat.Damage;//weaponDefence(this, index) - hb.curStat.Critical_Pierces_Defensive - hb.skills[index](hb, index);
                        break;
                    case 1:
                        damage = -hb.curStat.Weapon_Damage;//tacticalDefence(this, index) - hb.curStat.Critical_Pierces_Defensive - hb.skills[index](hb, index);
                        break;
                    case 2:
                        damage = -hb.curStat.Tactical_Damage;//tacticalDefence(this, index) - hb.curStat.Critical_Pierces_Defensive - hb.skills[index](hb, index);
                        break;
                }
            }
            if (damage < 0)
            { 
                curHealth += damage;
                isDamaged = true;
            }
            heroUI();
        }

        public void None                                   (ref Stat stat, Stat value) 
        {  }
        public void Strength                               (ref Stat stat, Stat value) 
        { stat.Strength += value.Strength; }
        public void Intelligence                           (ref Stat stat, Stat value) 
        {stat.Intelligence += value.Intelligence; }
        public void Agility                                (ref Stat stat, Stat value) 
        {stat.Agility += value.Agility; }
        public void Vital                                  (ref Stat stat, Stat value) 
        {stat.Vital += value.Vital; }
        public void Luck                                   (ref Stat stat, Stat value) 
        {stat.Luck += value.Luck; }
        public void CriticalImmunityRate                   (ref Stat stat, Stat value) 
        {stat.Critical_Immunity_Rate += value.Critical_Immunity_Rate; }
        public void CriticalDamage                         (ref Stat stat, Stat value) 
        {stat.Critical_Damage += value.Critical_Damage; }
        public void HealthPoint                            (ref Stat stat, Stat value) 
        {stat.Health_Point += value.Health_Point; }
        public void Damage                                 (ref Stat stat, Stat value) 
        {stat.Damage += value.Damage; }
        public void WeaponDamage                           (ref Stat stat, Stat value) 
        {stat.Weapon_Damage += value.Weapon_Damage; }
        public void TacticalDamage                         (ref Stat stat, Stat value) 
        {stat.Tactical_Damage += value.Tactical_Damage; }
        public void Defensive                              (ref Stat stat, Stat value) 
        {stat.Defensive += value.Defensive; }
        public void WeaponDefensive                        (ref Stat stat, Stat value) 
        {stat.Weapon_Defensive += value.Weapon_Defensive; }
        public void TacticalDefensive                      (ref Stat stat, Stat value) 
        {stat.Tactical_Defensive += value.Tactical_Defensive; }
        public void DamageBonus                            (ref Stat stat, Stat value) 
        {stat.Damage_Bonus += value.Damage_Bonus; }
        public void WeaponDamageBonus                      (ref Stat stat, Stat value) 
        {stat.Weapon_Damage_Bonus += value.Weapon_Damage_Bonus; }
        public void TacticalDamageBonus                    (ref Stat stat, Stat value) 
        {stat.Tactical_Damage_Bonus += value.Tactical_Damage_Bonus; }
        public void DamageMitigation                       (ref Stat stat, Stat value) 
        {stat.Damage_Mitigation += value.Damage_Mitigation; }
        public void WeaponDamageMitigation                 (ref Stat stat, Stat value) 
        {stat.Weapon_Damage_Mitigation += value.Weapon_Damage_Mitigation; }
        public void TacticalDamageMitigation               (ref Stat stat, Stat value) 
        {stat.Tactical_Damage_Mitigation += value.Tactical_Damage_Mitigation; }
        public void CriticalPiercesDefensive               (ref Stat stat, Stat value) 
        {stat.Critical_Pierces_Defensive += value.Critical_Pierces_Defensive; }
        public void FocusDamage                            (ref Stat stat, Stat value) 
        {stat.Focus_Damage += value.Focus_Damage; }
        public void ShieldBonusDamage                      (ref Stat stat, Stat value) 
        {stat.Shield_Bonus_Damage += value.Shield_Bonus_Damage; }
        public void DamageReflection                       (ref Stat stat, Stat value) 
        {stat.Damage_Reflection += value.Damage_Reflection; }
        public void ExtraHealingEffect                     (ref Stat stat, Stat value) 
        { stat.Extra_Healing_Effect += value.Extra_Healing_Effect; }

        //public void AddStrength(HeroBase stat, int index)
        //{
        //    float a = heroInfo.Strength;
        //}

        //public void AddIntelligence(HeroBase hb, int index)
        //{
        //    hb.damage = (hb.heroInfo.Attack + hb.heroInfo.Strength) * hb.heroInfo.Weapon_Attack;
        //}

        //public void WeaponDamage(HeroBase hb, int index)
        //{
        //    hb.damage = (hb.heroInfo.Attack + hb.heroInfo.Strength) * hb.heroInfo.Weapon_Attack;
        //}

        //public void WeaponDamageBonus(HeroBase hb, int index)
        //{
        //    hb.damage += (hb.heroInfo.Damage_Bonus + hb.heroInfo.Strength) * hb.heroInfo.Weapon_Damage_Bonus * 0.01f;
        //}

        //public void TacticalDamage(HeroBase hb, int index)
        //{
        //    hb.damage = (hb.heroInfo.Attack + hb.heroInfo.Intelligence) * hb.heroInfo.Tactical_Attack;
        //}

        //public void TacticalDamageBonus(HeroBase hb, int index)
        //{
        //    hb.damage += (hb.heroInfo.Damage_Bonus + hb.heroInfo.Intelligence) * hb.heroInfo.Tactical_Damage_Bonus * 0.01f;
        //}

        //public void WeaponDefence(HeroBase hb, int index)
        //{
        //    defence = (hb.heroInfo.Defensive + hb.heroInfo.Agility) * hb.heroInfo.Weapon_Defensive;
        //}

        //public void WeaponDefenceMitigation(HeroBase hb, int index)
        //{
        //    defence += (hb.heroInfo.Damage_Mitigation + hb.heroInfo.Agility) * hb.heroInfo.Weapon_Damage_Mitigation;
        //}

        //public void TacticalDefence(HeroBase hb, int index)
        //{
        //    defence = (hb.heroInfo.Defensive + hb.heroInfo.Agility) * hb.heroInfo.Tactical_Defensive;
        //}

        //public void TacticalDefenceMitigation(HeroBase hb, int index)
        //{
        //    defence += (hb.heroInfo.Damage_Mitigation + hb.heroInfo.Agility) * hb.heroInfo.Tactical_Damage_Mitigation;
        //}



        public int CompareTo(HeroBase x)
        {
            if (curStat.Luck > x.curStat.Luck)
                return 1;
            else if (curStat.Luck == x.curStat.Luck)
                return 0;
            else
                return -1;
        }

    }

}

