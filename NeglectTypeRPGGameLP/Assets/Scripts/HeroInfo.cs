using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroInfo : ScriptableObject
{
    public int id;
    public string Name;
    public string Faction;
    public string Type;
    public string Rarity;
    public string Explanation;
    public Sprite Icon;
    public string Model;

    public void CreateHeroInfoData(HeroInfo info, string[] value)
    {
        info.id             = int.Parse(value[0].Trim());
        info.Name           = value[1].Trim();
        info.Faction        = value[2].Trim();
        info.Type           = value[3].Trim();
        info.Rarity         = value[4].Trim();
        info.Explanation    = value[5].Trim();
        info.Icon = Resources.Load<Sprite>("Icon/"+value[6].Trim());
        info.Model          = value[7].Trim();
    }
}
