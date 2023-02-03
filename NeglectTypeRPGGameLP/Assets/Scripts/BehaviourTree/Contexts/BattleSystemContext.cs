using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class BattleSystemContext : Context
    {
        public bool isStart;

        [HideInInspector] public BattleState state;

        public GameObject[] startUI;
        public GameObject[] readyUI;
        public GameObject[] defaultUI;
        public GameObject[] endUI;

        public override void InitContext()
        {
            isStart = false;
            state = BattleState.Ready;
        }


    }

}
