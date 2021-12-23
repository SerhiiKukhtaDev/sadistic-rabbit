using Player;
using UnityEngine;

namespace Enemy.States.Transitions
{
    public class TargetReachRayTransition : RayTransition
    {
        protected override void Update()
        {
            {
                if (!(Vector3.Distance(transform.position, Target.Position) < rayDistance)) return;
                
                var hitInfo = Physics2D.Raycast(rayStartPoint.position,
                    rayStartPoint.right, contactFilter, HitResults, rayDistance);

                if (hitInfo != 0 && HitResults[0].collider.gameObject.TryGetComponent(out PlayerBase playerBase))
                {
                    NeedTransit = true;
                }
            }
        }
    }
}
