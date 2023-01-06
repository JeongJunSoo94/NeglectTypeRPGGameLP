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

    public List<RectTransform>  a= new List<RectTransform>();

    void Start() => TabClick(0);

    public void TabClick(int n)
    {
        for (int i = 0; i < Tab; i++)
        {
            //Tab[i].SetActive(i==n);
            TabBtnImage[i].sprite = i == n ? SelectSprite[i] : IdleSprite[i];
        }
    }
}
