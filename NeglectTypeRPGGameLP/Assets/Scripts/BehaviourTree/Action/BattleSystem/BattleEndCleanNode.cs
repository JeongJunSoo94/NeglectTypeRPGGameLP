using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class BattleEndCleanNode : ActionNode, ISystemNode
    {
        BattleSystemContext bsc;
        protected override void OnStart()
        {
            if (bsc == null)
                bsc = blackBoard.context as BattleSystemContext;
            Clear();
            bsc.isStart = false;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if(endUI())
                return State.Running;
            return State.Success;
        }

        public bool endUI()
        {

            bsc.resultText.text= "";
            blackBoard.data.winner = Team.None;
            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < blackBoard.data.RedHero.Count; ++i)
            {
                if(blackBoard.data.RedHero[i]!= null)
                    blackBoard.data.RedHero[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < blackBoard.data.BlueHero.Count; ++i)
            {
                if (blackBoard.data.BlueHero[i] != null)
                    blackBoard.data.BlueHero[i].gameObject.SetActive(true);
            }

            blackBoard.data.heroRedBattleList.Clear();
            blackBoard.data.heroBlueBattleList.Clear();

        }
    }
}