using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroStat : ScriptableObject
{
    public int id;
    public int DamageType;
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

    public void CreateHeroStatData(HeroStat stat, string[] value)
    {
       stat.id                           =  int.Parse(value[0].Trim() );
       stat.DamageType                   = (int)Enum.Parse(typeof(DamageType), value[1].Trim());
       stat.Combat_Power                 =  float.Parse(value[2].Trim() );
       stat.Strength                     =  float.Parse(value[3].Trim() );
       stat.Intelligence                 =  float.Parse(value[4].Trim() );
       stat.Agility                      =  float.Parse(value[5].Trim() );
       stat.Vital                        =  float.Parse(value[6].Trim() );
       stat.Luck                         =  float.Parse(value[7].Trim() );
       stat.Critical_Rate                =  float.Parse(value[8].Trim() );
       stat.Critical_Immunity_Rate       =  float.Parse(value[9].Trim() );
       stat.Critical_Damage              =  float.Parse(value[10].Trim() );
       stat.Health_Point                 =  float.Parse(value[11].Trim());
       stat.Attack                       =  float.Parse(value[12].Trim());
       stat.Weapon_Attack                =  float.Parse(value[13].Trim());
       stat.Tactical_Attack              =  float.Parse(value[14].Trim());
       stat.Defensive                    =  float.Parse(value[15].Trim());
       stat.Weapon_Defensive             =  float.Parse(value[16].Trim());
       stat.Tactical_Defensive           =  float.Parse(value[17].Trim());
       stat.Damage_Bonus                 =  float.Parse(value[18].Trim());
       stat.Weapon_Damage_Bonus          =  float.Parse(value[19].Trim());
       stat.Tactical_Damage_Bonus        =  float.Parse(value[20].Trim());
       stat.Damage_Mitigation            =  float.Parse(value[21].Trim());
       stat.Weapon_Damage_Mitigation     =  float.Parse(value[22].Trim());
       stat.Tactical_Damage_Mitigation   =  float.Parse(value[23].Trim());
       stat.Critical_Pierces_Defensive   =  float.Parse(value[24].Trim());
       stat.Focus_Damage                 =  float.Parse(value[25].Trim());
       stat.Shield_Bonus_Damage          =  float.Parse(value[26].Trim());
       stat.Damage_Reflection            =  float.Parse(value[27].Trim());
       stat.Extra_Healing_Effect         =  float.Parse(value[28].Trim());
    }
};
