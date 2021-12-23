using System.Collections.Generic;
using UnityEngine;

namespace Enemy.States.Transitions
{
    public class RayTransition : Transition
    {
        [SerializeField] protected ContactFilter2D contactFilter;
        [SerializeField] protected float rayDistance;
        [SerializeField] protected Transform rayStartPoint;

        protected List<RaycastHit2D> HitResults;

        private void Start()
        {
            HitResults = new List<RaycastHit2D>();
        }

        protected virtual void Update()
        {
        
        }
    }
}
