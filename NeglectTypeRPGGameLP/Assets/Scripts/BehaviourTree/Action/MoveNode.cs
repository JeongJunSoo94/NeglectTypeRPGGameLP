using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJS.BT
{
    public class MoveNode : ActionNode
    {
        HeroContext context;

        public bool isMove;
        bool success;

        protected override void OnStart()
        {
            if (context == null)
            {
                context = blackBoard.context as HeroContext;
            }
            TargetLockOn();
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if(success)
                return State.Success;
            if (isMove)
                return Action();

            return State.Failure;
        }

        public void TargetLockOn()
        {
            if (context.isRed)
            {
                for (int i = 0; i < context.bsc.BlueHero.Count; i++)
                {
                    if (context.bsc.BlueHero[i].GetComponent<HeroContext>().GetInfo().curHealth>0)
                    { 
                        context.target = context.bsc.BlueHero[i];
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < context.bsc.RedHero.Count; i++)
                {
                    if (context.bsc.RedHero[i].GetComponent<HeroContext>().GetInfo().curHealth > 0)
                    {
                        context.target = context.bsc.RedHero[i];
                        break;
                    }
                }
            }
            isMove = true;
            Debug.Log("Å¸°Ù ·Ï¿Â"+ context.target);
        }

        public State Action()
        {
            Debug.Log("¹«ºù");
            context.gameObject.transform.position = Vector3.Lerp(context.gameObject.transform.position, context.target.transform.position, 0.05f);

            if (Vector3.Distance(context.gameObject.transform.position, context.target.transform.position)<=50.0f)
            {
                isMove = false;
                return State.Success;
            }
            return State.Running;
        }
    }
}