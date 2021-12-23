using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerClimbing : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float climbingSpeed;
        [SerializeField] private Grid grid;
        [SerializeField] private Transform PlayerFrontPoint;

        private event Action<int> FirstClimb; 
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private bool _isClimbing;
        private bool _isLadder;
        private bool _isPositionNormalized;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ladder ladder))
            {
                _isLadder = true;
                FirstClimb += OnFirstClimb;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ladder ladder))
            {
                GetComponent<Animator>().SetBool("IsClimb", false);
                _isLadder = false;
                _isPositionNormalized = false;
                rigidbody2D.gravityScale = 1;
                _playerMovement.enabled = true;
                FirstClimb -= OnFirstClimb;
            }
        }

        private void Update()
        {
            _isClimbing = _isLadder && Math.Abs(_playerInput.VerticalAxis) > 0;
        }

        private void FixedUpdate()
        {
            var position = transform.position;
            var hitUp = Physics2D.Raycast(position, Vector2.up, 2, layerMask);

            if (_isClimbing && hitUp.collider != null)
            {
                FirstClimb?.Invoke(1);

                rigidbody2D.velocity = new Vector2(0, _playerInput.VerticalAxis * climbingSpeed);
            }
            else if(hitUp.collider != null && !_isClimbing)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
        }

        private void OnFirstClimb(int _)
        {
            if (_playerMovement.enabled)
                _playerMovement.enabled = false;
                
            TryNormalizePosition();
            rigidbody2D.gravityScale = 0;
            
            FirstClimb-=(OnFirstClimb);
        }

        private void TryNormalizePosition()
        {
            if (_isPositionNormalized) return;
            
            var enteredCellWorldPosition = grid.GetCellCenterWorld(grid.WorldToCell(PlayerFrontPoint.position));
            
            _isPositionNormalized = true;
            transform.position = enteredCellWorldPosition;
        }
    }
}
