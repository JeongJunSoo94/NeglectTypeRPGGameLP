using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("GameManager");
                instance = newGameObject.AddComponent<GameManager>();
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
        AllCharacterAdd();
    }
    
    void Start()
    {
    }

    void Update()
    {
        
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
