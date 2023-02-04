using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeglectTypeRPG
{
    public class BattleSystemContext : Context
    {
        public bool isStart;

        [HideInInspector] public BattleState state;

        public ObjectPoolQueue pool;

        public Text resultText;
        public GameObject[] readyUI;
        public Text[] combatDamageText;
        public override void InitContext()
        {
            isStart = false;
            state = BattleState.Ready;
        }

        public GameObject GetStateUI()
        {
            return pool.Dequeue();
        }

    }

}
