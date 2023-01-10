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
            {
                BSC = blackBoard.context as BattleSystemContext;
                BSB = blackBoard as BattleSystemBlackboard;
            }
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
            BSC.isRedTurn = true;
            BSC.redCount = BSC.RedHero.Count;
            BSC.blueCount = BSC.BlueHero.Count;
            for (int i = 0; i < BSC.redCount; i++)
            {
                BSC.heroRedBattleList.Enqueue(BSC.RedHero[i].GetComponent<HeroContext>());
                BSC.RedHero[i].GetComponent<HeroContext>().isRed = true;
                BSC.RedHero[i].GetComponent<HeroBlackBoard>().battleSystemBlackboard = blackBoard as BattleSystemBlackboard;
            }
            for (int i = 0; i < BSC.blueCount; i++)
            {
                BSC.heroBlueBattleList.Enqueue(BSC.BlueHero[i].GetComponent<HeroContext>());
                BSC.BlueHero[i].GetComponent<HeroContext>().isRed = true;
                BSC.BlueHero[i].GetComponent<HeroBlackBoard>().battleSystemBlackboard = blackBoard as BattleSystemBlackboard;
            }
           
        }
    }
}