using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.States.GoodPerson
{
    [RequireComponent(typeof(EnemyFacing))]
    public class GoodPersonFollowState : EventState
    {
        [SerializeField] private float followingSpeed;
        [Range(0, 1)] [SerializeField] private float speedOffset;
        
        private Rigidbody2D _rigidbody2D;
        private EnemyFacing _enemyFacing;
        private Animator _animator;
        private static readonly int IsAngry = Animator.StringToHash("IsAngry");

        private void Awake()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            followingSpeed = Random.Range(followingSpeed - speedOffset, followingSpeed);
        }

        protected override void OnEnable()
        {
            _animator.SetBool(IsAngry, true);
            _enemyFacing.TryFlipByTargetDistance(Target.Position);

            base.OnEnable();
        }

        private void Update()
        {
            _rigidbody2D.velocity = new Vector2(transform.right.x * followingSpeed, _rigidbody2D.velocity.y);
        }

        protected override void OnDisable()
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            
            base.OnDisable();
        }
        
    }
}
