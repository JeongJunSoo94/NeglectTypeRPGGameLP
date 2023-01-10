using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class TargetFindNode : ActionNode
    {
        HeroContext hc;
        Team myTeam;
        protected override void OnStart()
        {
            if (myTeam == Team.None)
            {
                myTeam = blackBoard.data.TeamCheck(blackBoard);
            }
            if(hc==null)
                hc = blackBoard.context as HeroContext;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return TargetLockOn();
        }

        public State TargetLockOn()
        {
            bool findTarget = false;
            if (myTeam == Team.RED)
            {
                for (int i = 0; i < blackBoard.data.BlueHero.Count; i++)
                {
                    if (blackBoard.data.BlueHero[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
                    {
                        hc.target = blackBoard.data.BlueHero[i].gameObject;
                        break;
                    }
                }
            }
            else if (myTeam == Team.BLUE)
            {
                for (int i = 0; i < blackBoard.data.RedHero.Count; i++)
                {
                    if (blackBoard.data.RedHero[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
                    {
                        hc.target = blackBoard.data.RedHero[i].gameObject;
                        break;
                    }
                }
            }
            if(findTarget)
                return State.Success;
            else
                return State.Failure;
            Debug.Log("Å¸°Ù ·Ï¿Â" + hc.target);
        }
    }
}