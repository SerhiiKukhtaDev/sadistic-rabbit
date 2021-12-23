using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Enemy.States.GroundEnemy
{
    [RequireComponent(typeof(EnemyFacing))]
    public class GroundEnemyIdleState : MonoBehaviour
    {
        [SerializeField] private float movingSpeed;
        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private float jumpForce;
        [SerializeField] private float timeToJump;
        [SerializeField] private float damage;
        
        private Rigidbody2D _rigidbody2D;
        private float _elapsedTime;
        private bool _isGround;
        private List<RaycastHit2D> _results;
        private bool _isWall;
        private EnemyFacing _enemyFacing;
        
        private void Start()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
            
            _results = new List<RaycastHit2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= timeToJump && _isGround)
            {
                _rigidbody2D.velocity = new Vector2( transform.right.x * movingSpeed, jumpForce);
                _elapsedTime = 0;
                _isGround = false;
            }

            if(_isGround)
                transform.Translate(Vector2.right * movingSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Grass _))
            {
                _isGround = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);
            if (other.TryGetComponent(out PlayerBase playerBase))
            {
                playerBase.ApplyDamage(damage);
            }
        }

        private void FixedUpdate()
        {
            var result = Physics2D.Raycast(transform.position, transform.right, 
                contactFilter, _results, 0.5f);

            _isWall = result > 0;

            if (_isWall)
            {
                _enemyFacing.Flip();
            }
        }
    }
}
