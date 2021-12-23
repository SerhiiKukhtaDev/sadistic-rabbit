using UnityEngine;

public class PoolCreator : MonoBehaviour
{
    public static Pool Create(string name, PooledObject pooledObject, Transform parent, int minCapacity, int maxCapacity,
        bool autoExpand)
    {
        Pool pool = new GameObject(name).AddComponent<Pool>();
        pool.Init(pooledObject, minCapacity, maxCapacity, autoExpand);
        pool.gameObject.transform.parent = parent;
        return pool;
    }
}
