using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    
    public class SkillAttackCheckNode : DecoratorNode
    {
        HeroContext HC;
        protected override void OnStart()
        {
            if (HC == null)
            {
                HC = blackBoard.context as HeroContext;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (HC.info.maxGage == HC.info.curSkillGage)
            {
                child.Update();
                return State.Running;
            }
            return State.Failure;
        }
    }
}