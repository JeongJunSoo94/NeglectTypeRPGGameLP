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

    public ObjectPool pool;
    public void CreateUI()
    {
        int count = DataManager.Instance.player.characterInventory.Count;
        for (int i = 0; i < count; i++)
        {
            if (DataManager.Instance.player.characterInventory[i])
            {
                GameObject obj = pool.Dequeue();
                obj.GetComponent<Image>().sprite = DataManager.Instance.heroInfo[i].Icon;
                obj.SetActive(true);
            }
        }
    }

    public void CharacterKind(int n)
    { 

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
