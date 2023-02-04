using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeglectTypeRPG
{
    public class TargetFollow : MonoBehaviour
    {
        public GameObject target;
        public Vector3 distance;
        public Slider hpBar;
        public Slider mpBar;

        private void OnDisable()
        {
        }
        void Update()
        {
            if (target)
            {
                transform.position = target.transform.position+ distance;
                if (!target.activeSelf)
                    gameObject.SetActive(false);
            }
        }
    }
}
