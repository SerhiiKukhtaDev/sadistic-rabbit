using System;
using UnityEngine;

namespace Enemy.States.Transitions
{
    [RequireComponent(typeof(EnemyFacing))]
    public class TargetReachOneSidedDirectRay : Transition
    {
        [SerializeField] private float distance;
        [SerializeField] private LayerMask layerMask;

        private void FixedUpdate()
        {
            var result = Physics2D.Raycast(transform.position, transform.right, 
                distance, layerMask);
                
            if(result.collider == null) return;

            if (result.collider.gameObject.Equals(Target.gameObject))
                NeedTransit = true;
        }
    }
}
