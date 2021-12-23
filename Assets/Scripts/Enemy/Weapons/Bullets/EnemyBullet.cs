using Player;
using UnityEngine;

namespace Enemy.Weapons.Bullets
{
    [RequireComponent(typeof(EnemyBullet))]
    public class EnemyBullet : Bullet
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerBase playerBase))
            {
                playerBase.ApplyDamage(damage);
            }
            
            PooledObject.ReturnToPool();
        }
    }
}
