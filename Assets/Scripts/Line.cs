using UnityEngine;
using Random = UnityEngine.Random;


public class Line : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform target;
    [SerializeField] private float targetPositionOffset;
    [SerializeField] private float timeBetweenChecking;
    [SerializeField] private LayerMask layerMask;
    [Range(0, 1)] [SerializeField] private float offset;
    private LineRenderer _lineRenderer;

    private float _randomOffset;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _randomOffset = Random.Range(-offset, offset / 4);
    }


    private void FixedUpdate()
    {
        var targetPosition = new Vector3(target.position.x, target.position.y + _randomOffset);   
        var startPointPosition = startPoint.position;

        float distance = (Vector3.Distance(targetPosition, startPointPosition) + targetPositionOffset);
        
        Vector3 direction = targetPosition - startPointPosition;

        Vector3 endPointPosition = startPointPosition + direction.normalized * distance;

        var result = Physics2D.Raycast(startPointPosition, direction.normalized, distance, layerMask);
        _lineRenderer.SetPosition(0, startPointPosition);
        
        if (result.collider != null)
        {
            _lineRenderer.SetPosition(1, result.point);
            return;
        }
       
        _lineRenderer.SetPosition(1, endPointPosition);
    }
}
