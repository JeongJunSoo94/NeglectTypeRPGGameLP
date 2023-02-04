using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NeglectTypeRPG
{
    public class TabManager : MonoBehaviour
    {
        public int Tab = 6;

        public Image[] TabBtnImage;

        public Sprite[] IdleSprite, SelectSprite;

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
                    HeroInfo info = DataManager.Instance.heroInfo[i];
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
            for (int i = 0; i < UIList.Count; ++i)
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
                TabBtnImage[i].color = i == n ? new Color(10, 10, 10, 0.1f) : new Color(10, 10, 10, 1f);
            }
            CharacterKind(n);
        }


    }

}
