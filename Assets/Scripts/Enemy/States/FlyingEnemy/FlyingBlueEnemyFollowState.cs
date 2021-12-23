using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy.States.FlyingEnemy
{
    public class FlyingBlueEnemyFollowState : EventState
    {
        [SerializeField] private float followingSpeed;
        [FormerlySerializedAs("offset")] [Range(0, 1)] [SerializeField] private float speedOffset;

        private EnemyFacing _enemyFacing;
        
        private void Awake()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
        }

        protected override void OnEnable()
        {
            _enemyFacing.SetAutoCheck(Target);
            
            base.OnEnable();
        }

        private void Start()
        {
            followingSpeed = Random.Range(followingSpeed - speedOffset, followingSpeed);
        }

        private void Update()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, Target.Position, Time.deltaTime * followingSpeed);
        }
    }
}
