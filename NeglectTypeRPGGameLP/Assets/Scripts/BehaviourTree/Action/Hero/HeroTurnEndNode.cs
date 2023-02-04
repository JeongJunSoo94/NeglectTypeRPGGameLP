using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class HeroTurnEndNode : ActionNode, ICharacterNode
    {
        HeroContext context;
        HeroBlackBoard hbb;
        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
                hbb = blackBoard as HeroBlackBoard;
            }
        }

        protected override void OnStop()
        {
            context.syncBehavior = 0;
            context.myTurn = false;
            BattleSystemContext con = hbb.battleSystemBlackboard.context as BattleSystemContext;
            con.state = BattleState.Battle;
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}