using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabManager : MonoBehaviour
{
    //public GameObject[] Tab;
    public int Tab=6;
    
    //�̹��� ���� üũ
    public Image[] TabBtnImage;

    //������ ���̽� �����ؾ���
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
