using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public enum TargetType
    {
        None,
        All,
        Proximate,
        Farthest,
        DefenceWeakest,
        HealthWeakest,
    }

    public class TargetFindNode :  ActionNode , ICharacterNode
    {
        HeroContext hc;
        Team myTeam;
        public TargetType type;
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
            if (findTarget)
                return State.Success;
            else
                return State.Failure;
        }

        public virtual bool TargetAdd(List<Blackboard> blackboards, HeroContext context)
        {
            for (int i = 0; i < blackboards.Count; i++)
            {
                if (blackboards[i] == null)
                    continue;
                if (TargetDecision(blackboards[i]))
                {
                    context.targets.Add(blackboards[i].gameObject);
                }
                context.targets.Add(blackboards[i].gameObject);
            }
            return context.targets.Count > 0 ?true:false;
        }

        public bool TargetDecision(Blackboard blackboard)
        {
            if(blackboard.GetComponent<HeroContext>().info.curHealth > 0)
                return true;

            return false;
        }
    }
}