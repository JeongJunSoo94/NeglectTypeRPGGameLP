using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class BattleTurnArrangementNode : ActionNode, ISystemNode
    {
        BattleSystemContext BSC;
        BattleSystemBlackboard BSB;
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
            int count = 0;
            for (int i = 0; i < blackBoard.data.RedHero.Count; i++)
            {
                if(blackBoard.data.RedHero[i]!=null)
                    ++count;
            }
            blackBoard.data.redCount = count;
            for (int i = 0; i < blackBoard.data.RedHero.Count; i++)
            {
                if (blackBoard.data.RedHero[i] == null)
                    continue;
                HeroContext hc = blackBoard.data.RedHero[i].GetComponent<HeroBlackBoard>().context as HeroContext;
                hc.myTurn = false;
                hc.Initialized();
                blackBoard.data.heroRedBattleList.Enqueue(blackBoard.data.RedHero[i].GetComponent<HeroContext>());
            }

        }
    }
}