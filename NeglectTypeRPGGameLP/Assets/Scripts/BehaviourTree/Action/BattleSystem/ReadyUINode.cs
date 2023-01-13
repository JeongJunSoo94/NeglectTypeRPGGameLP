using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class ReadyUINode : ActionNode
    {
        BattleSystemContext bsc;
        BattleSystemBlackboard bsb;
        public bool isOn;
        bool initialized = false;
        protected override void OnStart()
        {
            if (bsc == null)
                bsc = blackBoard.context as BattleSystemContext;
            if(bsb==null)
                bsb = blackBoard as BattleSystemBlackboard;
            ReadyUI(isOn);
            InitUI();
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

        public void ReadyUI(bool value)
        {
            for (int i = 0; i < bsc.readyUI.Length; ++i)
            {
                bsc.readyUI[i].SetActive(value);
            }
        }
        public void InitUI()
        {
            bsb.UI.TabClick(0);
            if(!initialized)
                ButtonUIInit();
        }

        public void ButtonUIInit()
        {
            initialized = true;
            for (int i = 0; i < bsb.UI.UIList.Count; ++i)
            {
                bsb.UI.UIList[i].collocate += new characterCollocate(GetCharacterCollocate);
            }
        }

        public void GetCharacterCollocate(int index, bool value)
        {
            GameObject character = DataManager.Instance.characterPool[index];
            if (value)
            {
                int i = 0;
                for (i = 0; i <= blackBoard.data.RedHero.Count; ++i)
                {
                    if (blackBoard.data.RedHero[i] == null)
                        break;
                }
                if (i < 5)
                {
                    blackBoard.data.RedHero[i] = character.GetComponent<Blackboard>();
                    character.transform.position = blackBoard.data.cell[i].transform.position;
                    character.GetComponent<HeroBlackBoard>().data = blackBoard.data;
                    character.GetComponent<HeroBlackBoard>().battleSystemBlackboard = bsb;
                    character.SetActive(true);
                    return;
                }
            }
            else
            {
                for (int i = 0; i < blackBoard.data.RedHero.Count; ++i)
                {
                    if (blackBoard.data.RedHero[i] == DataManager.Instance.characterPool[index].GetComponent<Blackboard>())
                    {
                        blackBoard.data.RedHero[i] = null;
                        DataManager.Instance.characterPool[index].SetActive(false);
                        return;
                    }
                }
            }

        }
    }
}