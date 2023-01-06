using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public int _id;
    public string _name;

    public List<HeroBase> characterInventory;
    public List<int> itemInventory;
    

}
