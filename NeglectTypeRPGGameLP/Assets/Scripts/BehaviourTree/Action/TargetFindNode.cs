using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{

    public class TargetFindNode :  ActionNode , ICharacterNode
    {
        HeroContext context;
        Team myTeam;
        public bool targetMyTeam;
        public StatType StatType;
        public TargetType targetType;
        [Range(1,5)]
        public int targetCount=1;
        protected override void OnStart()
        {
            if (myTeam == Team.None)
            {
                myTeam = blackBoard.data.TeamCheck(blackBoard);
            }
            if (context == null)
                context = blackBoard.context as HeroContext;
            context.targets.Clear();
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
            if (targetMyTeam)
            {
                if (myTeam == Team.RED)
                    findTarget = TargetAdd(blackBoard.data.RedHero, context);
                else if (myTeam == Team.BLUE)
                    findTarget = TargetAdd(blackBoard.data.BlueHero, context);
            }
            else
            {
                if (myTeam == Team.RED)
                    findTarget = TargetAdd(blackBoard.data.BlueHero, context);
                else if (myTeam == Team.BLUE)
                    findTarget = TargetAdd(blackBoard.data.RedHero, context);
            }
            if (findTarget)
                return State.Success;
            else
                return State.Failure;
        }

        public bool TargetAdd(List<Blackboard> blackboards, HeroContext context)
        {
            TargetTypeDecision(blackboards);
            return context.targets.Count > 0 ?true:false;
        }

        public float StatTypeDecision(Blackboard blackboard)
        {
            switch (StatType)
            {
                case StatType.Strength:
                    return blackboard.GetComponent<HeroContext>().info.curStat.Strength;
                case StatType.Intelligence:
                    return blackboard.GetComponent<HeroContext>().info.curStat.Intelligence;
                case StatType.Agility:
                    return blackboard.GetComponent<HeroContext>().info.curStat.Agility;
                case StatType.Vital:
                    return blackboard.GetComponent<HeroContext>().info.curStat.Vital;
                case StatType.Luck:
                    return blackboard.GetComponent<HeroContext>().info.curStat.Luck;
                case StatType.Health_Point:
                    return blackboard.GetComponent<HeroContext>().info.curHealth;
                case StatType.Defensive:
                    return blackboard.GetComponent<HeroContext>().info.curStat.Defensive;
            }
            return 0;
        }
        public void TargetTypeDecision(List<Blackboard> blackboards)
        {
            int count = 0;
            int index = -1;
            switch (targetType)
            {
                case TargetType.My:
                    context.targets.Add(blackBoard.gameObject);
                    context.targetPos = context.originPos;
                    break;
                case TargetType.All:
                    for (int i = 0; i < blackboards.Count; i++)
                    {
                        if (blackboards[i] == null)
                            continue;
                        if (blackboards[i].gameObject.GetComponent<HeroContext>().info.curHealth> 0)
                        {
                            context.targets.Add(blackboards[i].gameObject);
                        }
                        context.targets.Add(blackboards[i].gameObject);
                    }
                    break;
                case TargetType.Front:
                    for (int i = 0; i < blackboards.Count; ++i)
                    {
                        if (blackboards[i] == null)
                            continue;
                        if (blackboards[i].GetComponent<HeroContext>().info.curHealth > 0)
                        {
                            context.targets.Add(blackboards[i].gameObject);
                            count++;
                        }
                        if (count >= 1 && i == 1)
                        {
                            break;
                        }
                    }
                    break;
                case TargetType.Back:
                    for (int i = blackboards.Count-1; i >=0; --i)
                    {
                        if (blackboards[i] == null)
                            continue;
                        if (blackboards[i].GetComponent<HeroContext>().info.curHealth > 0)
                        {
                            context.targets.Add(blackboards[i].gameObject);
                            count++;
                        }
                        if (count >= 1 && i == 2)
                        {
                            break;
                        }
                    }
                    break;
                case TargetType.Proximate:

                    break;
                case TargetType.Farthest:

                    break;
                case TargetType.Strongest:
                    for (int i = 0; i < blackboards.Count; ++i)
                    {
                        if (blackboards[i] == null)
                            continue;
                        if (blackboards[i].GetComponent<HeroContext>().info.curHealth > 0)
                        {
                            if (index == -1)
                                index = i;
                            else if (StatTypeDecision(blackboards[i]) > StatTypeDecision(blackboards[index]))
                                index = i;
                        }
                    }
                    if (index != -1)
                        context.targets.Add(blackboards[index].gameObject);
                    break;
                case TargetType.Weakest:
                    for (int i = 0; i < blackboards.Count; ++i)
                    {
                        if (blackboards[i] == null)
                            continue;
                        if (blackboards[i].GetComponent<HeroContext>().info.curHealth > 0)
                        {
                            if (index == -1)
                                index = i;
                            else if (StatTypeDecision(blackboards[i]) < StatTypeDecision(blackboards[index]))
                                index = i;
                        }
                    }
                    if(index!=-1)
                        context.targets.Add(blackboards[index].gameObject);
                    break;
            }
        }
    }
}