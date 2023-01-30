using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class AttackBehaviorNode : ActionNode, ICharacterNode
    {
        HeroContext context;
        int attackIndex;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            if (context.info.maxMana == context.info.curMana)
            {
                context.skillAvailable = true;
            }
        }
        protected override void OnStop()
        {
            context.skillAvailable = false;
        }

        protected override State OnUpdate()
        {
            if (context.skillAvailable)
            {
                return Attack(1);
            }
            else
                return Attack(0);
        }

        private State Attack(int index)
        {
            attackIndex = index;
            context.AttackBehavior[index].rootNode.state = State.Running;
            context.AttackBehavior[index].treeState = State.Running;
            return context.AttackBehavior[index].Update();
        }
    }
}
