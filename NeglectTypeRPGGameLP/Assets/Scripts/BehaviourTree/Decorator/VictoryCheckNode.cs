using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class VictoryCheckNode : DecoratorNode
    {
        Team myTeam;
        protected override void OnStart()
        {
            if (myTeam == Team.None)
            {
                myTeam = blackBoard.data.TeamCheck(blackBoard);
            }
        }
        
        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (blackBoard.data.winner == myTeam)
            {
                child.Update();
                return State.Success;
            }
            return State.Failure;
        }
    }
}