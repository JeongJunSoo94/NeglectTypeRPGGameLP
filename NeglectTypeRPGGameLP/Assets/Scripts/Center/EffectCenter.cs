using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class EffectCenter : MonoBehaviour
    {
        public List<GameObject> buffs;
        public List<GameObject> debuffs;
        public List<GameObject> attacks;
        public List<GameObject> damages;
        public List<GameObject> heals;

        public ObjectPoolQueue Pool;

        public void GetVFX(int index, Vector3 pos)
        {
            GameObject obj = Pool.Dequeue();
            obj.transform.position = pos;
            obj.SetActive(true);
        }
    }
}
