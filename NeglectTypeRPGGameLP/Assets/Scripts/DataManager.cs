using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("GameManager");
                instance = newGameObject.AddComponent<DataManager>();
            }
            return instance;
        }
    }

    public HeroInfo[] heroInfo;
    public HeroStat[] heroStat;

    public PlayerData player;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //юс╫ц
        AllCharacterAdd();
    }
    
    public void AllCharacterAdd()
    {
        if (player.characterInventory.Count.Equals(0)&& player.characterInventory.Count< heroInfo.Length)
        { 
            for (int i = 0; i < heroInfo.Length; i++)
            {
                player.characterInventory.Add(true);
            }
        }
    }
}
