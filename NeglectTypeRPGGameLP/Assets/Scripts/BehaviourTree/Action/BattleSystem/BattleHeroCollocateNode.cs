using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleHeroCollocateNode : ActionNode
    {
        BattleSystemContext bsc;
        int visit = 0;
        protected override void OnStart()
        {
            if (bsc == null)
            {
                bsc = blackBoard.context as BattleSystemContext;
            }
            EnemyInit();
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

        public void EnemyInit()
        {
            blackBoard.data.blueCount = blackBoard.data.BlueHero.Count;
       
            for (int i = 0; i < blackBoard.data.BlueHero.Count; i++)
            {
                HeroBlackBoard hb = blackBoard.data.BlueHero[i].GetComponent<HeroBlackBoard>();
                hb.battleSystemBlackboard = blackBoard as BattleSystemBlackboard;
                hb.data = blackBoard.data;
                
                blackBoard.data.BlueHero[i].gameObject.SetActive(true);
                HeroContext hc = blackBoard.data.BlueHero[i].GetComponent<HeroBlackBoard>().context as HeroContext;
                hc.myTurn = false;
                hc.info.curHealth = 100;
                blackBoard.data.heroBlueBattleList.Enqueue(blackBoard.data.BlueHero[i].GetComponent<HeroContext>());
            }
            visit++;
        }
    }
}