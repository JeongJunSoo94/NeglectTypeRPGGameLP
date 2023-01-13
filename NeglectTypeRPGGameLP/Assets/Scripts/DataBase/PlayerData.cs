using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public int _id;
    public string _name;

    public List<bool> characterInventory;
    public List<int> itemInventory;

    public int GetCharacterInventoryTrue()
    {
        int count = 0;
        for (int i = 0; i < characterInventory.Count; ++i)
        {
            if(characterInventory[i])
                ++count;
        }
        return count;
    }
}
