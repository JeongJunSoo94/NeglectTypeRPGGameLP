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
            if (hc == null)
                hc = blackBoard.context as HeroContext;
            hc.targets.Clear();
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return TargetLockOn();
        }

        public void TargetFind()
        {

        }

        public State TargetLockOn()
        {
            bool findTarget = false;
            if (myTeam == Team.RED)
            {
                findTarget = TargetAdd(blackBoard.data.BlueHero, hc);
            }
            else if (myTeam == Team.BLUE)
            {
                findTarget = TargetAdd(blackBoard.data.RedHero, hc);
            }
            if(findTarget)
                return State.Success;
            else
                return State.Failure;
        }


        public bool TargetAdd(List<Blackboard> blackboards, HeroContext context)
        {
            for (int i = 0; i < blackboards.Count; i++)
            {
                if (blackboards[i] == null)
                    continue;
                if (blackboards[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
                {
                    context.targets.Add(blackboards[i].gameObject);
                }
            }
            return context.targets.Count > 0 ?true:false;
        }
    }
}