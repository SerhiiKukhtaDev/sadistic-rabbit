using Player;
using UnityEngine;

namespace Enemy.States.Transitions
{
    public class TargetLeftTwoSidedDirectRay : Transition
    {
        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private float distance;

        private void FixedUpdate()
        {
            Vector2 position = transform.position;
            Vector2 direction = transform.right;
            
            var raycastRight = 
                Physics2D.Raycast(position, direction, distance, contactFilter.layerMask);
            
            var raycastLeft = 
                Physics2D.Raycast(position, -direction, distance, contactFilter.layerMask);
            
            NeedTransit = !raycastLeft.collider.gameObject.Equals(Target.gameObject)
                          && !raycastRight.collider.gameObject.Equals(Target.gameObject);;
        }
    }
}
