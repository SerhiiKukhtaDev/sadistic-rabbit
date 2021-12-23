using UnityEngine;

public class AngleFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform from;
    [SerializeField] private float distanceToCheck;

    private void Update()
    {
        if(Vector3.Distance(@from.position, target.position) > distanceToCheck) return;

        var dir = target.position - from.position;

        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(z, Vector3.forward);
    }
}
