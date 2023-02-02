using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public class AssetsData : ScriptableObject
    {
        public List<Caches> heroCache;

        public void CreateInit(List<List<string[]>> value)
        {
            heroCache = new List<Caches>();
            for (int i = 0; i < value.Count; ++i)
            {
                Caches caches;
                caches.caches = new List<Cache>();
                for (int j = 0; j < value[i].Count; ++j)
                {
                    Cache cache;
                    cache.cache = value[i][j];
                    caches.caches.Add(cache);
                }
                heroCache.Add(caches);
            }
        }
    }
    [System.Serializable]
    public struct Caches
    {
        public List<Cache> caches;
    }

    [System.Serializable]
    public struct Cache
    {
        public string[] cache;
    }
}
