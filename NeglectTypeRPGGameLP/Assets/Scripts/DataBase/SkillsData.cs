using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    [System.Serializable]
    public struct SkillStatDataID
    {
        public List<SkillStatDataDamageID> skillStatDatas;
    }
    [System.Serializable]
    public struct SkillStatDataDamageID
    {
        public int damageID;
        public List<SkillDamageStats> skillDamageStats;
    }
    [System.Serializable]
    public struct SkillDamageStats
    {
        public int team;
        public int targetType;
        public int statType;
        public int attackState;
        public int round;
        public int turn;
        public int sync;
    }
    [System.Serializable]
    public struct StatDataID
    {
        public List<StatsDamageID> statDatas;
    }
    [System.Serializable]
    public struct StatsDamageID
    {
        public List<Stat> stats;
    }
    [System.Serializable]
    public struct Stat
    {
        //public int DamageID;
        public int Level;
        public float Strength;
        public float Intelligence;
        public float Agility;
        public float Vital;
        public float Luck;
        public float Critical_Rate;
        public float Critical_Immunity_Rate;
        public float Critical_Damage;
        public float Health_Point;
        public float Damage;
        public float Weapon_Damage;
        public float Tactical_Damage;
        public float Defensive;
        public float Weapon_Defensive;
        public float Tactical_Defensive;
        public float Damage_Bonus;
        public float Weapon_Damage_Bonus;
        public float Tactical_Damage_Bonus;
        public float Damage_Mitigation;
        public float Weapon_Damage_Mitigation;
        public float Tactical_Damage_Mitigation;
        public float Critical_Pierces_Defensive;
        public float Focus_Damage;
        public float Shield_Bonus_Damage;
        public float Damage_Reflection;
        public float Extra_Healing_Effect;
    }
    public class SkillsData : ScriptableObject
    {
        //이거 아님
        //스탯을 사용할수있게 아이디[] - 스탯데미지[] - 스탯들[]
        public List<StatDataID> statCache;
        public List<SkillStatDataID> statDamageCache;
        public void CreateSkillDamageStatsInit(List<string[]> value)
        {
            statDamageCache = new();
            int curId=1;
            for (int i = 0; i < value.Count; ++i)
            {
                int index = int.Parse(value[i][0].Trim());
                if (statDamageCache.Count < index)
                {
                    statDamageCache.Add(new());
                    SkillStatDataID stats = new();
                    stats.skillStatDatas = new();
                    statDamageCache[index-1] = stats;
                }
                if (curId != index)
                    curId = index;
                int damageIndex = int.Parse(value[i][1].Trim());
                if (statDamageCache[curId - 1].skillStatDatas.Count <= damageIndex)
                {
                    SkillStatDataDamageID skillstatdata = new();
                    skillstatdata.damageID = damageIndex;
                    skillstatdata.skillDamageStats = new ();
                    statDamageCache[curId - 1].skillStatDatas.Add(skillstatdata);
                }

                statDamageCache[curId - 1].skillStatDatas[damageIndex - 1].skillDamageStats.Add(CreateSkillDamageStat(value[i]));
            }
        }
        public void CreateSkillStatsInit(List<string[]> skillStats)
        {
            statCache = new ();
            for (int i = 0; i < skillStats.Count; ++i)
            {
                int index = int.Parse(skillStats[i][0].Trim()) - 1;
                if (statCache.Count <= index)
                    statCache.Add(new());
                int damageIndex = int.Parse(skillStats[i][1].Trim());
                if (statCache[index].statDatas == null)
                {
                    StatDataID statdatas = new();
                    statdatas.statDatas = new();
                    statCache[index] = statdatas;
                }
                if (statCache[index].statDatas.Count < damageIndex)
                {
                    statCache[index].statDatas.Add(new());
                }
                if(statCache[index].statDatas[damageIndex-1].stats==null)
                {
                    StatsDamageID stats = new();
                    stats.stats = new();
                    statCache[index].statDatas[damageIndex-1] = stats;
                }
                statCache[index].statDatas[damageIndex-1].stats.Add(CreateStatData(skillStats[i]));
            }
        }

        public SkillDamageStats CreateSkillDamageStat(string[] value)
        {
            SkillDamageStats stat       = new SkillDamageStats();
            //stat.damageID               = int.Parse(value[1].Trim());
            stat.team                   = (int)Enum.Parse(typeof(TargetTeam), value[2].Trim());
            stat.targetType             = (int)Enum.Parse(typeof(TargetType), value[3].Trim());
            stat.statType               = (int)Enum.Parse(typeof(StatType), value[4].Trim());
            stat.attackState            = (int)Enum.Parse(typeof(AttackState), value[5].Trim());
            stat.round                  = int.Parse(value[6].Trim());
            stat.turn                   = int.Parse(value[7].Trim());
            stat.sync                   = int.Parse(value[8].Trim());
            return stat;
        }


        public Stat CreateStatData(string[] value)
        {
             Stat stat = new();
             //stat.DamageID                   =   int.Parse(value[1].Trim());
             stat.Level                      =   int.Parse(value[2].Trim());
             stat.Strength                   = float.Parse(value[3].Trim());
             stat.Intelligence               = float.Parse(value[4].Trim());
             stat.Agility                    = float.Parse(value[5].Trim());
             stat.Vital                      = float.Parse(value[6].Trim());
             stat.Luck                       = float.Parse(value[7].Trim());
             stat.Critical_Rate              = float.Parse(value[8].Trim());
             stat.Critical_Immunity_Rate     = float.Parse(value[9].Trim());
             stat.Critical_Damage            = float.Parse(value[10].Trim());
             stat.Health_Point               = float.Parse(value[11].Trim());
             stat.Damage                     = float.Parse(value[12].Trim());
             stat.Weapon_Damage              = float.Parse(value[13].Trim());
             stat.Tactical_Damage            = float.Parse(value[14].Trim());
             stat.Defensive                  = float.Parse(value[15].Trim());
             stat.Weapon_Defensive           = float.Parse(value[16].Trim());
             stat.Tactical_Defensive         = float.Parse(value[17].Trim());
             stat.Damage_Bonus               = float.Parse(value[18].Trim());
             stat.Weapon_Damage_Bonus        = float.Parse(value[19].Trim());
             stat.Tactical_Damage_Bonus      = float.Parse(value[20].Trim());
             stat.Damage_Mitigation          = float.Parse(value[21].Trim());
             stat.Weapon_Damage_Mitigation   = float.Parse(value[22].Trim());
             stat.Tactical_Damage_Mitigation = float.Parse(value[23].Trim());
             stat.Critical_Pierces_Defensive = float.Parse(value[24].Trim());
             stat.Focus_Damage               = float.Parse(value[25].Trim());
             stat.Shield_Bonus_Damage        = float.Parse(value[26].Trim());
             stat.Damage_Reflection          = float.Parse(value[27].Trim());
             stat.Extra_Healing_Effect       = float.Parse(value[28].Trim());
            return stat;
        }
       
    }

}
