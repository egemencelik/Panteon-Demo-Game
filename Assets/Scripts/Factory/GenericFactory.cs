using System;
using System.Collections.Generic;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private T prefab;

    [SerializeField]
    private int poolAmount;

    private List<T> pooledObjects;

    private void Awake()
    {
        pooledObjects = new List<T>();
        T temp;
        for (var i = 0; i < poolAmount; i++)
        {
            temp = Instantiate(prefab, transform);
            temp.gameObject.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public T GetInstance()
    {
        for (var i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                pooledObjects[i].gameObject.SetActive(true);
                return pooledObjects[i];
            }
        }

        var temp = Instantiate(prefab, transform);
        pooledObjects.Add(temp);
        return temp;
    }
}