using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleTurnArrangementNode : ActionNode
    {
        BattleSystemContext BSC;
        BattleSystemBlackboard BSB;
        int visit = 0;
        protected override void OnStart()
        {
            if (BSC == null)
                BSC = blackBoard.context as BattleSystemContext;
            if (BSB == null)
                BSB = blackBoard as BattleSystemBlackboard;
            PlayerInit();
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

        public void PlayerInit()
        {
            blackBoard.data.isRedTurn = true;
            blackBoard.data.redCount = blackBoard.data.RedHero.Count;
            for (int i = 0; i < blackBoard.data.RedHero.Count; i++)
            {

                blackBoard.data.RedHero[i].GetComponent<HeroBlackBoard>().battleSystemBlackboard = blackBoard as BattleSystemBlackboard;
                blackBoard.data.RedHero[i].GetComponent<HeroBlackBoard>().data = blackBoard.data;
                blackBoard.data.RedHero[i].gameObject.SetActive(true);
                HeroContext hc = blackBoard.data.RedHero[i].GetComponent<HeroBlackBoard>().context as HeroContext;
                hc.myTurn = false;
                hc.info.curHealth = 100;
                blackBoard.data.heroRedBattleList.Enqueue(blackBoard.data.RedHero[i].GetComponent<HeroContext>());

            }
        }
    }
}