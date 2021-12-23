using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
