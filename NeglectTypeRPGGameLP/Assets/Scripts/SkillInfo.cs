using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public enum AttackState
    {
        Promptly,
        Duration
    }

    public enum SkillAction
    {
        Active,
        Passive
    }

    public class SkillInfo : ScriptableObject
    {
        public int ID;
        public string Name;
        public Sprite Icon;
        public string Explanation;
        public int action;
        public int BehaviorTreeID;
        public int damageType;
        public int attackState;
        public int Turn;

        public int level;
        public float Strength;
        public float Intelligence;
        public float Agility;
        public float Vital;
        public float Luck;
        public float Critical_Rate;
        public float Critical_Immunity_Rate;
        public float Critical_Damage;
        public float Health_Point;
        public float Attack;
        public float Weapon_Attack;
        public float Tactical_Attack;
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

        public void CreateSkillInfoData(string[] value)
        {
            ID                 = int.Parse(value[0].Trim());
            Name               = value[1].Trim();
            Icon               = Resources.Load<Sprite>("Icon/" + value[2].Trim());
            Explanation        = value[3].Trim();
            action             = (int)Enum.Parse(typeof(SkillAction), value[4].Trim());
            BehaviorTreeID     = int.Parse(value[5].Trim());
            damageType         = (int)Enum.Parse(typeof(DamageType), value[6].Trim());
            attackState        = (int)Enum.Parse(typeof(AttackState), value[7].Trim());
            Turn               = int.Parse(value[8].Trim());
        }

        public void CreateSkillStatData(string[] value)
        {
             level                      =   int.Parse(value[1].Trim());
             Strength                   = float.Parse(value[2].Trim());
             Intelligence               = float.Parse(value[3].Trim());
             Agility                    = float.Parse(value[4].Trim());
             Vital                      = float.Parse(value[5].Trim());
             Luck                       = float.Parse(value[6].Trim());
             Critical_Rate              = float.Parse(value[7].Trim());
             Critical_Immunity_Rate     = float.Parse(value[8].Trim());
             Critical_Damage            = float.Parse(value[9].Trim());
             Health_Point               = float.Parse(value[10].Trim());
             Attack                     = float.Parse(value[11].Trim());
             Weapon_Attack              = float.Parse(value[12].Trim());
             Tactical_Attack            = float.Parse(value[13].Trim());
             Defensive                  = float.Parse(value[14].Trim());
             Weapon_Defensive           = float.Parse(value[15].Trim());
             Tactical_Defensive         = float.Parse(value[16].Trim());
             Damage_Bonus               = float.Parse(value[17].Trim());
             Weapon_Damage_Bonus        = float.Parse(value[18].Trim());
             Tactical_Damage_Bonus      = float.Parse(value[19].Trim());
             Damage_Mitigation          = float.Parse(value[20].Trim());
             Weapon_Damage_Mitigation   = float.Parse(value[21].Trim());
             Tactical_Damage_Mitigation = float.Parse(value[22].Trim());
             Critical_Pierces_Defensive = float.Parse(value[23].Trim());
             Focus_Damage               = float.Parse(value[24].Trim());
             Shield_Bonus_Damage        = float.Parse(value[25].Trim());
             Damage_Reflection          = float.Parse(value[26].Trim());
             Extra_Healing_Effect       = float.Parse(value[27].Trim());
        }
    }
}
