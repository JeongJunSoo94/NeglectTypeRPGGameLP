using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool: MonoBehaviour
{
    public GameObject prefab;

    public Transform parent;

    public Queue<GameObject> useObjs;
    public Queue<GameObject> waitObjs;

    public int minCount;
    public int curCount;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        useObjs = new Queue<GameObject>();
        waitObjs = new Queue<GameObject>();
        Create();
    }

    public void Create()
    {
        for (int i = 0; i < minCount; i++)
        {
            if (parent)
                Enqueue(parent);
            else
                Enqueue();
        }
        curCount = minCount;
    }


    private void Enqueue()
    {
        GameObject obj = Instantiate(prefab);
        obj.GetComponent<HeroBattleIconUI>().parant = this;
        obj.SetActive(false);
        //waitObjs.Enqueue(obj);
        curCount++;
    }

    private void Enqueue(Transform parent)
    {
        GameObject obj = Instantiate(prefab, parent);
        obj.GetComponent<HeroBattleIconUI>().parant = this;
        obj.SetActive(false);
        //waitObjs.Enqueue(obj);
        curCount++;
    }

    public void Enqueue(GameObject obj)
    {
        waitObjs.Enqueue(obj);
        curCount++;
    }

    public GameObject Dequeue()
    {
        if (waitObjs.Count.Equals(0))
        {
            if (parent)
                Enqueue(parent);
            else
                Enqueue();
        }

        useObjs.Enqueue(waitObjs.Peek());
        curCount--;
        return waitObjs.Dequeue();
    }


}
