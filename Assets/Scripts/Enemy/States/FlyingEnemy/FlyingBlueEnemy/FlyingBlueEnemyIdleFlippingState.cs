using UnityEngine;

namespace Enemy.States.FlyingEnemy.FlyingBlueEnemy
{
    [RequireComponent(typeof(EnemyFacing))]
    public class FlyingBlueEnemyIdleFlippingState : EventState
    {
        [SerializeField] private float timeToFlip;
        
        private float _elapsedTime;
        private EnemyFacing _enemyFacing;

        private void Start()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
        }

        private void Update()
        {
            if (_elapsedTime >= timeToFlip)
            {
                _elapsedTime = 0;
                _enemyFacing.Flip();
            }
            
            _elapsedTime += Time.deltaTime;
        }
    }
}
