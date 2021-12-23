using UnityEngine;
using Player.Weapons.Base;

namespace Player.Weapons
{
    public class Rifle : Weapon
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootDelay;
        
        private void OnEnable()
        {
            _canShoot = true;
        }

        private bool _canShoot = true;
        
        public override void Shoot()
        {
            if (_canShoot)
            {
                Pool.GetFreeElement(shootPoint.position, shootPoint.rotation);
                _canShoot = false;
                StartCoroutine(Action.DelayedAction(shootDelay, () => _canShoot = true));
            }
        }
    }
}
