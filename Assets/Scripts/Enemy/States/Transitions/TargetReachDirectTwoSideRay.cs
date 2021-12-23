using Player;
using UnityEngine;

namespace Enemy.States.Transitions
{
    public class TargetReachDirectTwoSideRay : Transition
    {
        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private float distance;

        private void FixedUpdate()
        {
            var raycastRight = 
                Physics2D.Raycast(transform.position, transform.right, 
                    distance, contactFilter.layerMask);

            if (raycastRight.collider != null)
            {
                NeedTransit = raycastRight.collider.gameObject.Equals(Target.gameObject);
                if(NeedTransit) return;
            }
            
            var raycastLeft = 
                Physics2D.Raycast(transform.position, -transform.right, 
                    distance, contactFilter.layerMask);
            
            if (raycastLeft.collider != null)
            {
                NeedTransit = raycastLeft.collider.gameObject.Equals(Target.gameObject);
            }
        }
    }
}
