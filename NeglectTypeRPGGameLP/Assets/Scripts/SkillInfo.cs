using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public enum AttackState
    {
        DirectDamage,
        DamageOverTime
    }

    public enum SkillAction
    {
        Active,
        Passive
    }

    public enum StatType
    {
        None,
        Strength,
        Intelligence,
        Agility,
        Vital,
        Luck,
        Critical_Immunity_Rate,
        Critical_Damage,
        Health_Point,
        Attack,
        Weapon_Attack,
        Tactical_Attack,
        Defensive,
        Weapon_Defensive,
        Tactical_Defensive,
        Damage_Bonus,
        Weapon_Damage_Bonus,
        Tactical_Damage_Bonus,
        Damage_Mitigation,
        Weapon_Damage_Mitigation,
        Tactical_Damage_Mitigation,
        Critical_Pierces_Defensive,
        Focus_Damage,
        Shield_Bonus_Damage,
        Damage_Reflection,
        Extra_Healing_Effect
    }
    public enum TargetTeam
    {
        None,
        Alliance,
        Enemy
    }

    public enum TargetType
    {
        None,
        My,
        All,
        Front,
        Back,
        Proximate,
        Farthest,
        Strongest,
        Weakest,
    }

    
    public class SkillInfo : ScriptableObject
    {
        public int ID;
        public string Name;
        public Sprite Icon;
        public string Explanation;
        public int action;
        public int BehaviorTreeID;
        public int DependOnID;

        public List<SkillStatData> Stats;

        public void CreateSkillInfoData(string[] value)
        {
            ID                 = int.Parse(value[0].Trim());
            Name               = value[1].Trim();
            Icon               = Resources.Load<Sprite>("Icon/" + value[2].Trim());
            Explanation        = value[3].Trim();
            action             = (int)Enum.Parse(typeof(SkillAction), value[4].Trim());
            BehaviorTreeID     = int.Parse(value[5].Trim());
            DependOnID = int.Parse(value[6].Trim());
        }

        public static void CreateSkillInfoData(SkillInfo info,string[] value)
        {
            info.ID                 = int.Parse(value[0].Trim());
            info.Name               = value[1].Trim();
            info.Icon               = Resources.Load<Sprite>("Icon/" + value[2].Trim());
            info.Explanation        = value[3].Trim();
            info.action             = (int)Enum.Parse(typeof(SkillAction), value[4].Trim());
            info.BehaviorTreeID     = int.Parse(value[5].Trim());
            info.DependOnID         = int.Parse(value[6].Trim());
        }
        public static void CreateSkillStatData(SkillInfo info)
        {
        }
    }
}
