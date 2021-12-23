using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyBullet : Bullet
    {
        protected  void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.TryGetComponent(out PlayerBase playerBase))
            {
                playerBase.ApplyDamage(damage);
            }
        
            PooledObject.ReturnToPool();
        }
    }
}
