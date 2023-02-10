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
    public delegate void SkillDel(HeroBase stat, int skillIndex);
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
            //0 액티브 1 패시브
            //스킬의 액티브는 사용할때만 스킬의 패시브는 지속적
            //델리게이트 스킬 생성기
            if (info.action == 0)
            {
                actives.Add(CreateActive());
                //StatAdd(info, info.ID, info.Stats.Count);

            }
            else
            {
                //passives
            }
        }

        public SkillDel CreateActive()
        {
            //임시
            SkillDel ac = new SkillDel(GaugeUp);
            return ac;
        }

        public SkillDel CreatePassive()
        {
            //임시
            SkillDel ac = new SkillDel(GaugeUp);
            return ac;
        }

        public void AddOperator(SkillInfo stat, int attackIndex,int level)
        {
            for (int i = 0; i < stat.Stats.Count; ++i)
            {
                for (int j = 0; j < stat.Stats[i].skillDamageStats.Count; ++j)
                {
                    //stat.Stats[i].skillDamageStats[j].team;// 타겟 판정 노드에서 실행해도됨 
                    //stat.Stats[i].skillDamageStats[j].targetType;// 타겟을 어떻게 고를지 선택
                    //-----나눌수 있음
                    //stat.Stats[i].skillDamageStats[j].attackState;//스킬연산을 어떻게 할지 선택
                    //stat.Stats[i].skillDamageStats[j].statType;// 해당 스탯을 위의 연산에 따라서 처리함


                    //StatType(stat.Stats[i].skillDamageStats[j].statType, stat.Stats[i].skillDamageStats[j].attackState);

                    //델리게이트에 넣어줘야함
                    //그러면 어디서 함수를 긁어옴?
                    //switch (stat.Stats[i].skillDamageStats[j].statType)
                    //{
                    //    case 0:          break;
                    //    case 1:          break;//skillData.statCache[stat.ID].statDatas[stat.Stats[i].damageID].stats[level].Strength;
                    //    case 2:          break;
                    //    case 3:          break;
                    //    case 4:          break;
                    //    case 5:          break;
                    //    case 6:          break;
                    //    case 7:          break;
                    //    case 8:          break;
                    //    case 9:          break;
                    //    case 10:         break;//attacks[attackIndex] += new Attack(WeaponDamage); //데미지 연산을 할수있게 넣어줘야한다 그래야 상대방의 데미지 함수를 불러왔을때 연산이 된다.
                    //    case 11:         break;
                    //    case 12:         break;
                    //    case 13:         break;
                    //    case 14:         break;
                    //    case 15:         break;
                    //    case 16:         break;
                    //    case 17:         break;
                    //    case 18:         break;
                    //    case 19:         break;
                    //    case 20:         break;
                    //    case 21:         break;
                    //    case 22:         break;
                    //    case 23:         break;
                    //    case 24:         break;
                    //    case 25:         break;
                    //}
                }
            }
        }

        public void StatType(int statIndex, Type OperatorType)
        {
            switch (statIndex)
            {
                case 0:                         break;
                case 1:                         break;//skillData.statCache[stat.ID].statDatas[stat.Stats[i].damageID].stats[level].Strength;
                case 2:                         break;
                case 3:                         break;
                case 4:                         break;
                case 5:                         break;
                case 6:                         break;
                case 7:                         break;
                case 8:                         break;
                case 9:                         break;
                case 10:                        break;//attacks[attackIndex] += new Attack(WeaponDamage); //데미지 연산을 할수있게 넣어줘야한다 그래야 상대방의 데미지 함수를 불러왔을때 연산이 된다.
                case 11:                        break;
                case 12:                        break;
                case 13:                        break;
                case 14:                        break;
                case 15:                        break;
                case 16:                        break;
                case 17:                        break;
                case 18:                        break;
                case 19:                        break;
                case 20:                        break;
                case 21:                        break;
                case 22:                        break;
                case 23:                        break;
                case 24:                        break;
                case 25:                        break;
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

        //데미지에 무기,전술 두가지 종류의 데미지가 있다.
        //해당 타입의 공격이 오면 
        public void Damaged(HeroBase hb, int index)
        {
            //hb.actives[index](hb, skillIndex);
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



        //public void Strength                               (Stat stat) {curStat.Strength += }
        public void Intelligence                           (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Intelligence += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Intelligence; }
        public void Agility                                (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Agility += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Agility; }
        public void Vital                                  (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Vital += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Vital; }
        public void Luck                                   (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Luck += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Luck; }
        public void CriticalImmunityRate                   (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Critical_Immunity_Rate += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Critical_Immunity_Rate; }
        public void CriticalDamage                         (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Critical_Damage += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Critical_Damage; }
        public void HealthPoint                            (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Health_Point += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Health_Point; }
        public void Damage                                 (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Damage += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Damage; }
        public void WeaponDamage                           (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Weapon_Damage += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Weapon_Damage; }
        public void TacticalDamage                         (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Tactical_Damage += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Tactical_Damage; }
        public void Defensive                              (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Defensive += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Defensive; }
        public void WeaponDefensive                        (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Weapon_Defensive += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Weapon_Defensive; }
        public void TacticalDefensive                      (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Tactical_Defensive += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Tactical_Defensive; }
        public void DamageBonus                            (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Damage_Bonus += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Damage_Bonus; }
        public void WeaponDamageBonus                      (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Weapon_Damage_Bonus += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Weapon_Damage_Bonus; }
        public void TacticalDamageBonus                    (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Tactical_Damage_Bonus += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Tactical_Damage_Bonus; }
        public void DamageMitigation                       (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Damage_Mitigation += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Damage_Mitigation; }
        public void WeaponDamageMitigation                 (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Weapon_Damage_Mitigation += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Weapon_Damage_Mitigation; }
        public void TacticalDamageMitigation               (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Tactical_Damage_Mitigation += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Tactical_Damage_Mitigation; }
        public void CriticalPiercesDefensive               (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Critical_Pierces_Defensive += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Critical_Pierces_Defensive; }
        public void FocusDamage                            (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Focus_Damage += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Focus_Damage; }
        public void ShieldBonusDamage                      (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Shield_Bonus_Damage += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Shield_Bonus_Damage; }
        public void DamageReflection                       (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Damage_Reflection += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Damage_Reflection; }
        public void ExtraHealingEffect                     (HeroBase hero,SkillInfo skill, SkillStatDataDamageID damageStats,int level)   {curStat.Extra_Healing_Effect += skillData.statCache[skill.ID].statDatas[damageStats.damageID].stats[level].Extra_Healing_Effect;}
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

