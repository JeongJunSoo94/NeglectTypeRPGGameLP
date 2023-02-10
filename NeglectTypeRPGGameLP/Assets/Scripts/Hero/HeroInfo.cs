using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeglectTypeRPG;

public enum DamageType
{
    Weapon,
    Tactical
}

public enum HeroType
{
    Shooter,
    Warrior,
    Guardian
}

public enum Faction
{
    Minutemen,
    Vindicators,
    Wildlings,
    Watchers
}

[System.Serializable]
public class HeroInfo : ScriptableObject
{
    public int id;
    public string Name;
    public int faction;
    public int Type;
    public string Rarity;
    public string Explanation;
    public Sprite Icon;
    public string Model;

    public int level;
    public float CombatPower;
    public Stat defaultStat;

    //public float Strength;
    //public float Intelligence;
    //public float Agility;
    //public float Vital;
    //public float Luck;
    //public float Critical_Rate;
    //public float Critical_Immunity_Rate;
    //public float Critical_Damage;
    //public float Health_Point;
    //public float Damage;
    //public float Weapon_Damage;
    //public float Tactical_Damage;
    //public float Defensive;
    //public float Weapon_Defensive;
    //public float Tactical_Defensive;
    //public float Damage_Bonus;
    //public float Weapon_Damage_Bonus;
    //public float Tactical_Damage_Bonus;
    //public float Damage_Mitigation;
    //public float Weapon_Damage_Mitigation;
    //public float Tactical_Damage_Mitigation;
    //public float Critical_Pierces_Defensive;
    //public float Focus_Damage;
    //public float Shield_Bonus_Damage;
    //public float Damage_Reflection;
    //public float Extra_Healing_Effect;

    public SkillInfo normalAttack;
    public List<SkillInfo> skills;

    public void CreateHeroInfoData(string[] value)
    {
        id             = int.Parse(value[0].Trim());
        Name           = value[1].Trim();
        faction        = (int)Enum.Parse(typeof(Faction),value[2].Trim());
        Type           = (int)Enum.Parse(typeof(HeroType), value[3].Trim());
        Rarity         = value[4].Trim();
        Explanation    = value[5].Trim();
        Icon           = Resources.Load<Sprite>("Icon/"+value[6].Trim());
        Model          = value[7].Trim();
    }

    public static void CreateHeroInfoData(HeroInfo info,string[] value)
    {
        info.id = int.Parse(value[0].Trim());
        info.Name = value[1].Trim();
        info.faction = (int)Enum.Parse(typeof(Faction), value[2].Trim());
        info.Type = (int)Enum.Parse(typeof(HeroType), value[3].Trim());
        info.Rarity = value[4].Trim();
        info.Explanation = value[5].Trim();
        info.Icon = Resources.Load<Sprite>("Icon/" + value[6].Trim());
        info.Model = value[7].Trim();
    }


    //public void CreateHeroStatData(string[] value)
    //{
    //    //level                      =   int.Parse(value[1].Trim());
    //    //Combat_Power               = float.Parse(value[2].Trim());
    //    Strength                   = float.Parse(value[3].Trim());
    //    Intelligence               = float.Parse(value[4].Trim());
    //    Agility                    = float.Parse(value[5].Trim());
    //    Vital                      = float.Parse(value[6].Trim());
    //    Luck                       = float.Parse(value[7].Trim());
    //    Critical_Rate              = float.Parse(value[8].Trim());
    //    Critical_Immunity_Rate     = float.Parse(value[9].Trim());
    //    Critical_Damage            = float.Parse(value[10].Trim());
    //    Health_Point               = float.Parse(value[11].Trim());
    //    Damage                     = float.Parse(value[12].Trim());
    //    Weapon_Damage              = float.Parse(value[13].Trim());
    //    Tactical_Damage            = float.Parse(value[14].Trim());
    //    Defensive                  = float.Parse(value[15].Trim());
    //    Weapon_Defensive           = float.Parse(value[16].Trim());
    //    Tactical_Defensive         = float.Parse(value[17].Trim());
    //    Damage_Bonus               = float.Parse(value[18].Trim());
    //    Weapon_Damage_Bonus        = float.Parse(value[19].Trim());
    //    Tactical_Damage_Bonus      = float.Parse(value[20].Trim());
    //    Damage_Mitigation          = float.Parse(value[21].Trim());
    //    Weapon_Damage_Mitigation   = float.Parse(value[22].Trim());
    //    Tactical_Damage_Mitigation = float.Parse(value[23].Trim());
    //    Critical_Pierces_Defensive = float.Parse(value[24].Trim());
    //    Focus_Damage               = float.Parse(value[25].Trim());
    //    Shield_Bonus_Damage        = float.Parse(value[26].Trim());
    //    Damage_Reflection          = float.Parse(value[27].Trim());
    //    Extra_Healing_Effect       = float.Parse(value[28].Trim());
    //}
    public static void CreateHeroStatData(HeroInfo info,string[] value)
    {
        info.level = int.Parse(value[1].Trim());
        info.defaultStat.Strength                   = float.Parse(value[2].Trim());
        info.defaultStat.Intelligence               = float.Parse(value[3].Trim());
        info.defaultStat.Agility                    = float.Parse(value[4].Trim());
        info.defaultStat.Vital                      = float.Parse(value[5].Trim());
        info.defaultStat.Luck                       = float.Parse(value[6].Trim());
        info.defaultStat.Critical_Rate              = float.Parse(value[7].Trim());
        info.defaultStat.Critical_Immunity_Rate     = float.Parse(value[8].Trim());
        info.defaultStat.Critical_Damage            = float.Parse(value[9].Trim());
        info.defaultStat.Health_Point               = float.Parse(value[10].Trim());
        info.defaultStat.Damage                     = float.Parse(value[11].Trim());
        info.defaultStat.Weapon_Damage              = float.Parse(value[12].Trim());
        info.defaultStat.Tactical_Damage            = float.Parse(value[13].Trim());
        info.defaultStat.Defensive                  = float.Parse(value[14].Trim());
        info.defaultStat.Weapon_Defensive           = float.Parse(value[15].Trim());
        info.defaultStat.Tactical_Defensive         = float.Parse(value[16].Trim());
        info.defaultStat.Damage_Bonus               = float.Parse(value[17].Trim());
        info.defaultStat.Weapon_Damage_Bonus        = float.Parse(value[18].Trim());
        info.defaultStat.Tactical_Damage_Bonus      = float.Parse(value[19].Trim());
        info.defaultStat.Damage_Mitigation          = float.Parse(value[20].Trim());
        info.defaultStat.Weapon_Damage_Mitigation   = float.Parse(value[21].Trim());
        info.defaultStat.Tactical_Damage_Mitigation = float.Parse(value[22].Trim());
        info.defaultStat.Critical_Pierces_Defensive = float.Parse(value[23].Trim());
        info.defaultStat.Focus_Damage               = float.Parse(value[24].Trim());
        info.defaultStat.Shield_Bonus_Damage        = float.Parse(value[25].Trim());
        info.defaultStat.Damage_Reflection          = float.Parse(value[26].Trim());
        info.defaultStat.Extra_Healing_Effect       = float.Parse(value[27].Trim());
    }

    public void CombatPowerInit()
    {
        CombatPower = defaultStat.Strength +
                       defaultStat.Intelligence +
                       defaultStat.Agility +
                       defaultStat.Vital +
                       defaultStat.Luck +
                       defaultStat.Critical_Rate +
                       defaultStat.Critical_Immunity_Rate +
                       defaultStat.Critical_Damage +
                       defaultStat.Health_Point +
                       defaultStat.Damage +
                       defaultStat.Weapon_Damage +
                       defaultStat.Tactical_Damage +
                       defaultStat.Defensive +
                       defaultStat.Weapon_Defensive +
                       defaultStat.Tactical_Defensive +
                       defaultStat.Damage_Bonus +
                       defaultStat.Weapon_Damage_Bonus +
                       defaultStat.Tactical_Damage_Bonus +
                       defaultStat.Damage_Mitigation +
                       defaultStat.Weapon_Damage_Mitigation +
                       defaultStat.Tactical_Damage_Mitigation +
                       defaultStat.Critical_Pierces_Defensive +
                       defaultStat.Focus_Damage +
                       defaultStat.Shield_Bonus_Damage +
                       defaultStat.Damage_Reflection +
                       defaultStat.Extra_Healing_Effect;
    }
}
