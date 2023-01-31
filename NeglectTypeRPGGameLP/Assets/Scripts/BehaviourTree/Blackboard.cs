using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace NeglectTypeRPG
{
    [System.Serializable]
    public class Blackboard : MonoBehaviour
    {
        [HideInInspector]
        public Context context;
        public BattleDataCenter data;

        public virtual void Awake()
        {
            Init();
        }

        public void Init()
        {
            context = GetComponent<Context>();
            context.InitContext();
        }

    }
}
