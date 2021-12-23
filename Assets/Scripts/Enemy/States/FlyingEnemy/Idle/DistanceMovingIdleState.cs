using System;
using System.Collections;
using UnityEngine;

namespace Enemy.States.FlyingEnemy.Idle
{
    public class DistanceMovingIdleState : EventState
    {
        [SerializeField] private float speed;
        [SerializeField] private float movingToOneSideTime;
        [SerializeField] private LayerMask layerMask;
        
        private EnemyFacing _enemyFacing;
        private float _elapsedTime;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            _elapsedTime = 0;
        }
        
        private void Awake()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
        }
        
        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= movingToOneSideTime)
            {
                _elapsedTime = 0;
                ChangeDirection();
            }
            
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        private void ChangeDirection()
        {
            _enemyFacing.Flip();
        }

        private void FixedUpdate()
        {
            var result = Physics2D.Raycast(transform.position, 
                transform.right, 1, layerMask);

            if (result.collider != null)
            {
                ChangeDirection();
                _elapsedTime = 0;
            }
        }
    }
}
