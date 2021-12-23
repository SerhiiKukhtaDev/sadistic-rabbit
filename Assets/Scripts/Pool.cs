using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
        private Transform _container;
        private PooledObject _pooledObject;
        private int _minCapacity, _maxCapacity;
        private bool _autoExpand;
        
        private List<PooledObject> _pooledObjects;

        public void Init(PooledObject pooledObject, int minCapacity, int maxCapacity, bool autoExpand)
        {
            _pooledObject = pooledObject;
            _container = transform;
            _autoExpand = autoExpand;
            _minCapacity = minCapacity;
            _maxCapacity = _autoExpand ? int.MaxValue : maxCapacity;
            
            CreatePool();
        }
    
        private void CreatePool()
        {
            _pooledObjects = new List<PooledObject>(_minCapacity);
    
            for (int i = 0; i < _minCapacity; i++)
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
    
            if (_autoExpand)
                return CreateElement(true);
    
            if (_pooledObjects.Count < _maxCapacity)
                return CreateElement(true);
    
            throw new Exception("Pool is over");
        }
    
        private PooledObject CreateElement(bool isActiveByDefault = false)
        {
            var created = Instantiate(_pooledObject, _container);
                
            created.gameObject.SetActive(isActiveByDefault);
            _pooledObjects.Add(created);
    
            return created;
        }
    
}
