using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class BattleTurnBehaviorNode : ActionNode
    {
        BattleSystemContext BSC;
        protected override void OnStart()
        {
            if (BSC == null)
            {
                BSC = blackBoard.context as BattleSystemContext;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            switch (BSC.state)
            {
                case BattleState.Battle:
                    {
                        if (BattlePossibleCheck())
                        {
                            BattleOneTurn();
                            return State.Running;
                        }
                    }
                    break;
                case BattleState.BattleWaitTurn:
                    {
                        if (BattlePossibleCheck())
                        {
                            return State.Running;
                        }
                    }
                    break;

                //case BattleState.BattleEndTurn:
                //    {
                //        if (BattlePossibleCheck())
                //        {
                //            BattleOneTurn();
                //            return State.Running;
                //        }
                //    }
                //    break;
            }
            return State.Success;
        }

        void BattleOneTurn()
        {
            HeroContext hero;
            if (blackBoard.data.isRedTurn)
            {
                if (blackBoard.data.heroRedBattleList.Count().Equals(0))
                {
                    blackBoard.data.isRedTurn = false;
                    ResetHero(blackBoard.data.RedHero, blackBoard.data.heroRedBattleList);
                }
                else
                { 
                    hero = blackBoard.data.heroRedBattleList.Dequeue();
                    if (hero.info.curHealth != 0)
                    {
                        hero.myTurn = true;
                        BSC.state = BattleState.BattleWaitTurn;
                        //BSC.heroRedBattleList.Enqueue(hero);
                    }
                }
            }
            else
            {
                if (blackBoard.data.heroBlueBattleList.Count().Equals(0))
                {
                    blackBoard.data.isRedTurn = true;
                    ResetHero(blackBoard.data.BlueHero, blackBoard.data.heroBlueBattleList);
                }
                else
                {
                    hero = blackBoard.data.heroBlueBattleList.Dequeue();
                    if (hero.info.curHealth != 0)
                    {
                        hero.myTurn = true;
                        BSC.state = BattleState.BattleWaitTurn;
                        //BSC.heroBlueBattleList.Enqueue(hero);
                    }
                }
            }
        }

        bool BattlePossibleCheck()
        {
            if (blackBoard.data.redCount.Equals(0))
            {
                blackBoard.data.winner = Team.BLUE;
                return false;
            }
            if (blackBoard.data.blueCount.Equals(0))
            {
                blackBoard.data.winner = Team.RED;
                return false;
            }

            return true;
        }

        void ResetHero(List<Blackboard> list, PriorityQueue<HeroContext> q)
        {
            for (int i = 0; i < list.Count; i++)
            {
                q.Enqueue(list[i].gameObject.GetComponent<HeroContext>());
            }
        }
       
    }
}