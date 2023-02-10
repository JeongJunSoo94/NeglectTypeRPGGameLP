using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeglectTypeRPG;
using TMPro;

namespace NeglectTypeRPG
{
    public class BattleHeroCollocateNode : ActionNode, ISystemNode
    {
        BattleSystemContext bsc; 
        BattleSystemBlackboard bsb;
        bool initialized = false;
        float redCombat=0;
        float blueCombat = 0;
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
                bsb.UI.UIList[i].collocate += new characterCollocate(BattleCombatPowerUI);
            }
        }

        public void BattleCombatPowerUI(int index, bool value)
        {
            bsc.combatDamageText[1].text = redCombat + "전투력";
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
                    character.transform.position = blackBoard.data.redCell[i].transform.position;
                    HeroBlackBoard heroBlackBoard = character.GetComponent<HeroBlackBoard>();
                    heroBlackBoard.data = blackBoard.data;
                    heroBlackBoard.battleSystemBlackboard = bsb;
                    character.transform.rotation = Quaternion.Euler(0, 180, 0);
                    character.GetComponent<HeroContext>().originRotation = new Vector3(0,180,0);
                    character.SetActive(true);
                    redCombat += character.GetComponent<HeroContext>().info.heroInfo.CombatPower;
                    if(!character.GetComponent<HeroContext>().statBar)
                        character.GetComponent<HeroContext>().UIAdd(bsc.pool.Dequeue());
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
                        redCombat -= character.GetComponent<HeroContext>().info.heroInfo.CombatPower;
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
                hc.syncBehavior = 0;
                hc.myTurn = false;
                hc.Initialized();
            }
        }

        public void EnemyInit()
        {
            blueCombat = 0;
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
                hc.syncBehavior = 0;
                hc.myTurn = false;
                hc.Initialized();
                blueCombat += hc.info.heroInfo.CombatPower;
                blackBoard.data.heroBlueBattleList.Enqueue(blackBoard.data.BlueHero[i].GetComponent<HeroContext>());
                if (!hc.statBar)
                    hc.UIAdd(bsc.pool.Dequeue());
            }
            bsc.combatDamageText[0].text = blueCombat + "전투력";
        }

     
    }
}