using System.Collections.Generic;
using System.Linq;
using Enemy.States.Transitions;
using UnityEngine;

namespace Enemy.States
{
    public class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> transitions;

        protected EnemyTarget Target;
        
        public void Enter()
        {
            if (enabled) return;
            
            foreach (var transition in transitions)
            {
                transition.enabled = true;
                transition.TrySetTarget(Target);
            }

            enabled = true;
        }

        public void Exit()
        {
            if(!enabled) return;

            foreach (var transition in transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }

        public State GetNextState()
        {
            foreach (var transition in transitions.Where(t => t.NeedTransit))
            {
                return transition.TargetState;
            }

            return null;
        }

        public void TrySetTarget(EnemyTarget target)
        {
            if (Target != null) return;

            Target = target;
        }
    }
}
