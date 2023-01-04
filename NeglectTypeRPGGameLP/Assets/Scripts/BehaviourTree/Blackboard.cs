using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JJS.BT
{
    [System.Serializable]
    public class Blackboard : MonoBehaviour
    {
        public Context context;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            context.InitContext();
        }
    }

}
