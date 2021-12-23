using Enemy;
using UnityEngine;

namespace Player.Weapons
{
    public class PlayerBullet : Bullet
    {
        [SerializeField] private LayerMask layerMask;
        
        private Vector3 _lastPosition;

        protected override void OnEnable()
        {
            _lastPosition = transform.position;
            base.OnEnable();
        }

        protected override void Update()
        {
            var hitInfo = Physics2D.Raycast(transform.position, transform.right, 0.5f, layerMask);
            
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.TryGetComponent(out EnemyBase enemyBase))
                {
                    enemyBase.ApplyDamage(damage);
                }
                
                PooledObject.ReturnToPool();
            }

            base.Update();
        }
    }
}
