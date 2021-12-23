using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float liveTime;
    
    protected PooledObject PooledObject;

    protected virtual void Start()
    {
        PooledObject = GetComponent<PooledObject>();
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(CheckForLiveTimeEnd());   
    }

    protected void OnDisable()
    {
        StopCoroutine(nameof(CheckForLiveTimeEnd));
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private IEnumerator CheckForLiveTimeEnd()
    {
        var waitForSeconds = new WaitForSeconds(liveTime);
        yield return waitForSeconds;
        
        PooledObject.ReturnToPool();
    }
}
