using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleHeroCollocateNode : ActionNode
    {
        BattleSystemContext bsc; 
        BattleSystemBlackboard bsb;
        int visit = 0; 
        bool initialized = false;
        protected override void OnStart()
        {
            if (bsc == null)
            {
                bsc = blackBoard.context as BattleSystemContext;
            }
            if (bsb == null)
                bsb = blackBoard as BattleSystemBlackboard;
            PlayerInit();
            EnemyInit();
            InitUI();
        }
        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (bsc.state == BattleState.Ready)
            {
                if (!bsc.isStart)
                    return State.Running;
            }
            return State.Success;
        }

        public void InitUI()
        {
            bsb.UI.TabClick(0);
            if (!initialized)
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

        public void PlayerInit()
        {
            for (int i = 0; i < blackBoard.data.RedHero.Count; i++)
            {
                if (blackBoard.data.RedHero[i] == null)
                    continue;
                HeroContext hc = blackBoard.data.RedHero[i].GetComponent<HeroBlackBoard>().context as HeroContext;
                hc.myTurn = false;
                hc.Initialized();
                blackBoard.data.RedHero[i] = null;
            }
        
        }

        public void EnemyInit()
        {
            int count = 0;
            for (int i = 0; i < blackBoard.data.BlueHero.Count; i++)
            {
                if (blackBoard.data.BlueHero[i] != null)
                    ++count;
            }

            blackBoard.data.blueCount = count;
       
            for (int i = 0; i < blackBoard.data.BlueHero.Count; i++)
            {
                HeroBlackBoard hb = blackBoard.data.BlueHero[i].GetComponent<HeroBlackBoard>();
                hb.battleSystemBlackboard = blackBoard as BattleSystemBlackboard;
                hb.data = blackBoard.data;
                
                blackBoard.data.BlueHero[i].gameObject.SetActive(true);
                HeroContext hc = blackBoard.data.BlueHero[i].GetComponent<HeroBlackBoard>().context as HeroContext;
                hc.myTurn = false;
                hc.Initialized();
                blackBoard.data.heroBlueBattleList.Enqueue(blackBoard.data.BlueHero[i].GetComponent<HeroContext>());
            }
            visit++;
        }
    }
}