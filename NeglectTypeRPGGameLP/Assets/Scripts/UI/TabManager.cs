using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabManager : MonoBehaviour
{
    //public GameObject[] Tab;
    public int Tab=6;
    
    //이미지 갯수 체크
    public Image[] TabBtnImage;

    //데이터 베이스 접근해야함
    public Sprite[] IdleSprite, SelectSprite;

    //public List<RectTransform>  a= new List<RectTransform>();

    public ObjectPoolList pool;

    public List<HeroBattleIconUI> UIList;

    private void Start()
    {
        CreateUI();
    }
    public void CreateUI()
    {
        int count = DataManager.Instance.player.characterInventory.Count;
        pool._objcetCount = DataManager.Instance.player.GetCharacterInventoryTrue();
        pool.Production();
        for (int i = 0; i < count; i++)
        {
            if (DataManager.Instance.player.characterInventory[i])
            {
                GameObject obj = pool.GetPooledObject();
                HeroBattleIconUI hbiui = obj.GetComponent<HeroBattleIconUI>();
                HeroInfo info =DataManager.Instance.heroInfo[i];
                hbiui.heroIndex = i;
                hbiui.faction = info.faction;
                obj.GetComponent<Image>().sprite = info.Icon;
                UIList.Add(hbiui);
                obj.SetActive(true);
            }
        }
    }

    public void CharacterKind(int n)
    {
        for (int i=0;i< UIList.Count; ++i)
        {
            if (n == 0)
            {
                UIList[i].gameObject.SetActive(true);
                continue;
            }
            if (UIList[i].faction == n - 1)
                UIList[i].gameObject.SetActive(true);
            else
                UIList[i].gameObject.SetActive(false);
        }
    }

    public void TabClick(int n)
    {
        for (int i = 0; i < Tab; i++)
        {
            //Tab[i].SetActive(i==n);
            //TabBtnImage[i].sprite = i == n ? SelectSprite[i] : IdleSprite[i];
            TabBtnImage[i].color = i == n ?  new Color(10, 10, 10, 0.1f) : new Color(10, 10, 10, 1f);
        }
        CharacterKind(n);
    }


}
