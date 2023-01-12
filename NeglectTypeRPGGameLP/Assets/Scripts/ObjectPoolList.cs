using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolList : MonoBehaviour
{
    public GameObject parentObject = null;
    public GameObject _object;

    public int _objcetCount;
    private List<GameObject> _objectPools;

    public void Production()
    {
        _objectPools = new List<GameObject>();
        for (int i = 0; i < _objcetCount; i++)
        {
            GameObject obj = Instantiate(_object, parentObject.transform);
            obj.SetActive(false);
            _objectPools.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _objectPools.Count; i++)
        {
            if (!_objectPools[i].activeInHierarchy)
            {
                return _objectPools[i];
            }
        }
        GameObject obj = Instantiate(_object, parentObject.transform);
        ++_objcetCount;
        obj.SetActive(false);
        _objectPools.Add(obj);
        return obj;
    }

}
