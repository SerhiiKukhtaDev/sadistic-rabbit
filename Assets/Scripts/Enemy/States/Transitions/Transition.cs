using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy.States.Transitions
{
    public class Transition : MonoBehaviour
    {
        [FormerlySerializedAs("targetEnemyState")] [SerializeField] private State targetState;

        public State TargetState => targetState;
    
        public bool NeedTransit { get; protected set; }

        protected EnemyTarget Target;

        protected virtual void OnDisable()
        {
            if(NeedTransit)
                NeedTransit = false;
        }

        public void TrySetTarget(EnemyTarget target)
        {
            if(Target != null) return;

            Target = target;
        }
    }
}
