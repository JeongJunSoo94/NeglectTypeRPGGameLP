using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolQueue: MonoBehaviour
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
        waitObjs.Enqueue(obj);
        obj.SetActive(false);
        curCount++;
    }

    private void Enqueue(Transform parent)
    {
        GameObject obj = Instantiate(prefab, parent);
        waitObjs.Enqueue(obj);
        obj.SetActive(false);
        curCount++;
    }

    //public void Enqueue(GameObject obj)
    //{
    //    waitObjs.Enqueue(obj);
    //    curCount++;
    //}

    private void UsedDequeue()
    {
        for (int i = 0; i < useObjs.Count; ++i)
        {
            if (useObjs.Peek().activeSelf)
            {
                useObjs.Enqueue(useObjs.Dequeue());
            }
            else 
            {
                waitObjs.Enqueue(useObjs.Dequeue());
                curCount++;
            }
        }
        if (waitObjs.Count == 0)
        {
            if (parent)
                Enqueue(parent);
            else
                Enqueue();
        }
    }

    public GameObject Dequeue()
    {
        if (waitObjs.Count.Equals(0))
        {
            if (useObjs.Count.Equals(0))
            {
                if (parent)
                    Enqueue(parent);
                else
                    Enqueue();
            }
            else
            {
                UsedDequeue();
            }
        }

        useObjs.Enqueue(waitObjs.Peek());
        curCount--;
        return waitObjs.Dequeue();
    }

}
