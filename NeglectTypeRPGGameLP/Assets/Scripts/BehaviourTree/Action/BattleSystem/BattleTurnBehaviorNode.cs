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
            return State.Failure;
        }

        void BattleOneTurn()
        {
            HeroContext hero;
            if (BSC.isRedTurn)
            {
                if (BSC.heroRedBattleList.Count().Equals(0))
                {
                    BSC.isRedTurn = false;
                    ResetHero(BSC.RedHero, BSC.heroRedBattleList);
                }
                else
                { 
                    hero = BSC.heroRedBattleList.Dequeue();
                    if (hero.info.curHealth != 0)
                    {
                        hero.myTurn = true;
                        TargetLockOn(hero);
                        BSC.state = BattleState.BattleWaitTurn;
                        //BSC.heroRedBattleList.Enqueue(hero);
                    }
                }
            }
            else
            {
                if (BSC.heroBlueBattleList.Count().Equals(0))
                {
                    BSC.isRedTurn = true;
                    ResetHero(BSC.BlueHero, BSC.heroBlueBattleList);
                }
                else
                {
                    hero = BSC.heroBlueBattleList.Dequeue();
                    if (hero.info.curHealth != 0)
                    {
                        hero.myTurn = true;
                        TargetLockOn(hero);
                        BSC.state = BattleState.BattleWaitTurn;
                        //BSC.heroBlueBattleList.Enqueue(hero);
                    }
                }
            }
        }

        bool BattlePossibleCheck()
        {
            if (BSC.redCount.Equals(0))
            {
                BSC.winner = Win.BLUE;
                BSC.state = BattleState.End;
                return false;
            }
            if (BSC.blueCount.Equals(0))
            {
                BSC.winner = Win.RED;
                BSC.state = BattleState.End;
                return false;
            }

            return true;
        }

        void ResetHero(List<GameObject> list, PriorityQueue<HeroContext> q)
        {
            for (int i = 0; i < list.Count; i++)
            {
                q.Enqueue(list[i].GetComponent<HeroContext>());
            }
        }

        public void TargetLockOn(HeroContext hero)
        {
            if (hero.isRed)
            {
                for (int i = 0; i < BSC.BlueHero.Count; i++)
                {
                    if (BSC.BlueHero[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
                    {
                        hero.target = BSC.BlueHero[i];
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < BSC.RedHero.Count; i++)
                {
                    if (BSC.RedHero[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
                    {
                        hero.target = BSC.RedHero[i];
                        break;
                    }
                }
            }
            Debug.Log("Å¸°Ù ·Ï¿Â" + hero.target);
        }
    }
}