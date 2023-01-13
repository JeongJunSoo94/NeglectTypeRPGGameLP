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
    public GameObject defaultCharacter;
    public HeroInfo[] heroInfo;
    public HeroStat[] heroStat;
    public List<GameObject> model = new List<GameObject>();

    public List<GameObject> characterPool;

    public PlayerData player;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //임시
        //AllCharacterAdd();
        Init();
    }

    public void Init()
    {
        characterPool = new List<GameObject>(new GameObject[heroInfo.Length]);
        PlayerCharacterCreate();
    }

    public void CreateCharacter(int charIndex)
    {
        GameObject obj = Instantiate(defaultCharacter,transform);
        HeroBase hb= new HeroBase();
        hb.heroInfo = Instantiate(heroInfo[charIndex]);
        hb.heroStat = Instantiate(heroStat[charIndex]);
        obj.GetComponent<HeroContext>().info = hb;
        Instantiate(model[charIndex], obj.transform);
        obj.SetActive(false);
        characterPool[charIndex] = obj;
    }

    public void PlayerCharacterCreate()
    {
        for (int i = 0; i < heroInfo.Length; ++i)
        {
            if(player.characterInventory[i] && characterPool[i]==null)
                CreateCharacter(i);
        }
    }

    //임시 모든캐릭터 넣어주기
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
