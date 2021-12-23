using UnityEngine;

namespace Enemy.States.Transitions
{
    public class TargetReachedTransition : Transition
    {
        [SerializeField] private float distance;
        
        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, Target.Position) < distance)
            {
                NeedTransit = true;
            }
        }
    }
}
