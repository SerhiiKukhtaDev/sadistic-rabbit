using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float minGroundNormalY = .65f;
        [SerializeField] private Vector2 velocity;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;

        private Vector2 _targetVelocity;
        private PlayerInput _playerInput;
        private bool _grounded;
        private Vector2 _groundNormal;
        private Rigidbody2D _rigidbody2d;
        private ContactFilter2D _contactFilter;
        private readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
        private readonly List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
        private Animator _animator;

        private const float MinMoveDistance = 0.001f;
        private const float ShellRadius = 0.01f;
        
        private static readonly int XVelocity = Animator.StringToHash("XVelocity");
        private static readonly int YVelocity = Animator.StringToHash("YVelocity");

        private void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            
            _contactFilter.useTriggers = false;
            _contactFilter.SetLayerMask(layerMask);
            _contactFilter.useLayerMask = true;
        }

        public void DoJump()
        {
            velocity.y = jumpForce;  
        }

        private void Update()
        {
            _targetVelocity = new Vector2(_playerInput.HorizontalAxis * speed, 0);

            if (_playerInput.SpacePressed && _grounded)
            {
                DoJump();
            }

            _animator.SetFloat(XVelocity, velocity.x);
            _animator.SetFloat(YVelocity, velocity.y);
        }
        
        private void FixedUpdate()
        {
            velocity += Physics2D.gravity * Time.deltaTime;
            velocity.x = _targetVelocity.x;

            _grounded = false;

            Vector2 deltaPosition = velocity * Time.deltaTime;
            Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);
        }

        private void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > MinMoveDistance)
            {
                int count = _rigidbody2d.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

                _hitBufferList.Clear();

                for (int i = 0; i < count; i++)
                {
                    _hitBufferList.Add(_hitBuffer[i]);
                }

                foreach (var hit in _hitBufferList)
                {
                    Vector2 currentNormal = hit.normal;
                    if (currentNormal.y > minGroundNormalY)
                    {
                        _grounded = true;
                        if (yMovement)
                        {
                            _groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(velocity, currentNormal);
                    
                    if (projection < 0)
                    {
                        velocity -= projection * currentNormal;
                    }

                    float modifiedDistance = hit.distance - ShellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            _rigidbody2d.position += move.normalized * distance;

            if (_grounded) velocity.y = 0;
        }
    }
}
