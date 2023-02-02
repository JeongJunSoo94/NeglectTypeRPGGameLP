using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class DamageInfo : ScriptableObject
    {
        public int id;
        public int DamageType;

        public int turnCount;
 
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
    }
}
