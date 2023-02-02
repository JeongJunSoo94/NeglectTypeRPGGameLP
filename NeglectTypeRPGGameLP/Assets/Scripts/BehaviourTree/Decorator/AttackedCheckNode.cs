using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class AttackedCheckNode : DecoratorNode
    {
        HeroContext context;
        float curCount;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            Debug.Log(blackBoard.gameObject.name+"나는 맞았어");
        }

        protected override void OnStop()
        {
            context.info.damageCount--;
        }

        protected override State OnUpdate()
        {
            if (curCount == context.info.damageCount)
                return child.Update();
            return State.Failure;
        }
    }
}
