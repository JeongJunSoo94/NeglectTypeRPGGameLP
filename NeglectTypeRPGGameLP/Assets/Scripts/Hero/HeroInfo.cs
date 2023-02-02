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
    public string Type;
    public string Rarity;
    public string Explanation;
    public Sprite Icon;
    public string Model;
    public int DamageType;

    public int level;
    public float Combat_Power;
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

    public void CreateHeroInfoData(string[] value)
    {
        id             = int.Parse(value[0].Trim());
        Name           = value[1].Trim();
        faction        = (int)Enum.Parse(typeof(Faction),value[2].Trim());
        Type           = value[3].Trim();
        Rarity         = value[4].Trim();
        Explanation    = value[5].Trim();
        Icon           = Resources.Load<Sprite>("Icon/"+value[6].Trim());
        Model          = value[7].Trim();
        DamageType     = (int)Enum.Parse(typeof(DamageType), value[8].Trim());
    }

    public void CreateHeroStatData(string[] value)
    {
        level                      =   int.Parse(value[1].Trim());
        Combat_Power               = float.Parse(value[2].Trim());
        Strength                   = float.Parse(value[3].Trim());
        Intelligence               = float.Parse(value[4].Trim());
        Agility                    = float.Parse(value[5].Trim());
        Vital                      = float.Parse(value[6].Trim());
        Luck                       = float.Parse(value[7].Trim());
        Critical_Rate              = float.Parse(value[8].Trim());
        Critical_Immunity_Rate     = float.Parse(value[9].Trim());
        Critical_Damage            = float.Parse(value[10].Trim());
        Health_Point               = float.Parse(value[11].Trim());
        Attack                     = float.Parse(value[12].Trim());
        Weapon_Attack              = float.Parse(value[13].Trim());
        Tactical_Attack            = float.Parse(value[14].Trim());
        Defensive                  = float.Parse(value[15].Trim());
        Weapon_Defensive           = float.Parse(value[16].Trim());
        Tactical_Defensive         = float.Parse(value[17].Trim());
        Damage_Bonus               = float.Parse(value[18].Trim());
        Weapon_Damage_Bonus        = float.Parse(value[19].Trim());
        Tactical_Damage_Bonus      = float.Parse(value[20].Trim());
        Damage_Mitigation          = float.Parse(value[21].Trim());
        Weapon_Damage_Mitigation   = float.Parse(value[22].Trim());
        Tactical_Damage_Mitigation = float.Parse(value[23].Trim());
        Critical_Pierces_Defensive = float.Parse(value[24].Trim());
        Focus_Damage               = float.Parse(value[25].Trim());
        Shield_Bonus_Damage        = float.Parse(value[26].Trim());
        Damage_Reflection          = float.Parse(value[27].Trim());
        Extra_Healing_Effect       = float.Parse(value[28].Trim());
    }
}
