using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private PooledObject pooledObject;
    [SerializeField] private int minCapacity, maxCapacity;
    [SerializeField] private bool autoExpand;
    
    private List<PooledObject> _pooledObjects;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _pooledObjects = new List<PooledObject>(minCapacity);

        for (int i = 0; i < minCapacity; i++)
        {
            CreateElement();
        }
    }

    private bool TryGetElement(out PooledObject element)
    {
        foreach (var item in _pooledObjects.Where(item => !item.gameObject.activeInHierarchy))
        {
            element = item;
            element.gameObject.SetActive(true);

            return true;
        }
        
        element = null;
        return false;
    }

    public PooledObject GetFreeElement(Vector3 position)
    {
        var element = GetFreeElement();

        element.transform.position = position;
        return element;
    }
    
    public PooledObject GetFreeElement(Vector3 position, Quaternion rotation)
    {
        var element = GetFreeElement(position);

        element.transform.rotation = rotation;
        
        return element;
    }

    public PooledObject GetFreeElement()
    {
        if (TryGetElement(out var element))
        {
            return element;
        }

        if (autoExpand)
            return CreateElement(true);

        if (_pooledObjects.Count < maxCapacity)
            return CreateElement(true);

        throw new Exception("Pool is over");
    }

    private PooledObject CreateElement(bool isActiveByDefault = false)
    {
        var created = Instantiate(pooledObject, container);
            
        created.gameObject.SetActive(isActiveByDefault);
        _pooledObjects.Add(created);

        return created;
    }
    
    

    private void OnValidate()
    {
        if (maxCapacity < minCapacity)
            maxCapacity = minCapacity;
        
        if(autoExpand)
            maxCapacity = int.MaxValue;
    }
}
