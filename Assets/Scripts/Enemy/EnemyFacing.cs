using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyFacing : MonoBehaviour
    {
        [SerializeField] private bool facingRight = true;
        [SerializeField] private UnityEvent<bool> onFlip;

        private bool _autoTargetCheck;
        private EnemyTarget _target;
        public bool FacingRight => facingRight;

        private void Start()
        {
            if(!facingRight) onFlip?.Invoke(true);
        }

        public void TryFlipByTargetDistance(Vector3 targetPosition)
        {
            if (transform.position.x > targetPosition.x && facingRight)
            {
                Flip();
            }
            else if(transform.position.x < targetPosition.x && !facingRight) Flip();
        }
        
        public void SetAutoCheck(EnemyTarget target)
        {
            _target = target;
            _autoTargetCheck = true;
        }

        public void DisableAutoCheck()
        {
            _target = null;
            _autoTargetCheck = false;
        }

        private void FixedUpdate()
        {
            if(!_autoTargetCheck) return;
            
            TryFlipByTargetDistance(_target.Position);
        }

        public void Flip()
        {
            transform.Rotate(0, 180, 0);
            facingRight = !facingRight;

            onFlip?.Invoke(facingRight);
        }
    }
}
