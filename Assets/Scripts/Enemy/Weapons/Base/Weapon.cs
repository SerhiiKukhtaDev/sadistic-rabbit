using UnityEngine;

namespace Enemy.Weapons.Base
{
    [RequireComponent(typeof(ObjectPool))]
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform shootPoint;
        
        protected ObjectPool ObjectPool;

        protected virtual void Start()
        {
            ObjectPool = GetComponent<ObjectPool>();
        }

        public abstract void TryShoot();
    }
}
