using Enemy.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [FormerlySerializedAs("startEnemyState")] [SerializeField] private State startState;
        [SerializeField] private EnemyTarget target;
        
        private State _currentState;

        private void Start()
        {
            ChangeState(startState);
        }

        private void Update()
        {
            var nextState = _currentState.GetNextState();

            if (nextState != null)
            {
                ChangeState(nextState);
            } 
        }

        private void ChangeState(State nextState)
        {
            if(_currentState != null)
                _currentState.Exit();

            _currentState = nextState;
        
            _currentState.TrySetTarget(target);
            _currentState.Enter();
        }
    }
}
