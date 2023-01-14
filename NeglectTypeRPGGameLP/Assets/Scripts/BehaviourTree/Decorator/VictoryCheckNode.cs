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
                if(child.Update()==State.Running)
                    return State.Running;
                return State.Success;
            }
            return State.Failure;
        }
    }
}