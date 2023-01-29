using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    
    public class SkillAttackCheckNode : DecoratorNode
    {
        HeroContext HC;
        bool skill;
        protected override void OnStart()
        {
            if (HC == null)
            {
                HC = blackBoard.context as HeroContext;
            }
            if (HC.info.maxMana == HC.info.curMana)
            {
                skill = true;
            }
        }

        protected override void OnStop()
        {
            skill = false;
        }

        protected override State OnUpdate()
        {
            if (skill)
            {
                child.Update();
                return State.Running;
            }
            return State.Failure;
        }
    }
}