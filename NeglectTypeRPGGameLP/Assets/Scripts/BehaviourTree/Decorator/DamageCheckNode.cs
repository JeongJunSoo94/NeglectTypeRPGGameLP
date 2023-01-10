using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class DamageCheckNode : DecoratorNode
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
            if (HC.info.curHealth <= 0)
            {
                //if (HC.isRed)
                //    HC.bsc.redCount--;
                //else
                //    HC.bsc.blueCount--;

                HC.gameObject.SetActive(false);
            }
            if (HC.info.prevHealth != HC.info.curHealth)
            {
                HC.info.prevHealth = HC.info.curHealth;
            }
                //if (HC.info.prevHealth != HC.info.curHealth)
                //{
                //    child.Update();
                //    return State.Running;
                //}
           return State.Failure;
        }
    }
}