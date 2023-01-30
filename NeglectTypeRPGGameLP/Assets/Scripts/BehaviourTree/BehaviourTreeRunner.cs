using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NeglectTypeRPG
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public BehaviourTree tree;

        void Start()
        {
            tree = tree.Clone(GetComponent<Blackboard>());
        }

        // Update is called once per frame
        void Update()
        {
            tree.Update();
        }
    }
}
