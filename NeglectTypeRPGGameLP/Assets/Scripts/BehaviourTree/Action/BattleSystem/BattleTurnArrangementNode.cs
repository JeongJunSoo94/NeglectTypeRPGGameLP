using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleTurnArrangementNode : ActionNode
    {
        BattleSystemContext BSC;
        BattleSystemBlackboard BSB;
        protected override void OnStart()
        {
            if (BSC == null)
                BSC = blackBoard.context as BattleSystemContext;
            if (BSB == null)
                BSB = blackBoard as BattleSystemBlackboard;
            Init();
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (BSB.isStart)
            { 
                return State.Success;
            }
            return State.Running;
        }

        public void Init()
        {
            blackBoard.data.isRedTurn = true;
            blackBoard.data.redCount = blackBoard.data.RedHero.Count;
            blackBoard.data.blueCount = blackBoard.data.BlueHero.Count;
            for (int i = 0; i < blackBoard.data.redCount; i++)
            {
                blackBoard.data.RedHero[i].GetComponent<HeroBlackBoard>().battleSystemBlackboard = blackBoard as BattleSystemBlackboard;
                blackBoard.data.RedHero[i].GetComponent<HeroBlackBoard>().data = blackBoard.data;
                //임시
                blackBoard.data.RedHero[i].gameObject.SetActive(true);
                //
                blackBoard.data.heroRedBattleList.Enqueue(blackBoard.data.RedHero[i].GetComponent<HeroContext>());
            }
            for (int i = 0; i < blackBoard.data.blueCount; i++)
            {
                blackBoard.data.BlueHero[i].GetComponent<HeroBlackBoard>().battleSystemBlackboard = blackBoard as BattleSystemBlackboard;
                blackBoard.data.BlueHero[i].GetComponent<HeroBlackBoard>().data = blackBoard.data;
                //임시
                blackBoard.data.BlueHero[i].gameObject.SetActive(true);
                //
                blackBoard.data.heroBlueBattleList.Enqueue(blackBoard.data.BlueHero[i].GetComponent<HeroContext>());
            }
           
        }
    }
}