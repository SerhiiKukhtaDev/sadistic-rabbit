using System.Collections.Generic;
using Player.Weapons.Base;
using UnityEngine;


namespace Player.Weapons
{
    public class MachineGun : Weapon
    {
        [SerializeField] private List<Transform> shootPoints;
        [SerializeField] private float shootDelay;

        private bool _canShoot = true;

        private void OnEnable()
        {
            _canShoot = true;
        }

        public override void Shoot()
        {
            if (!_canShoot) return;
            
            foreach (var shootPoint in shootPoints)
            {
                Pool.GetFreeElement(shootPoint.position, shootPoint.rotation);
                _canShoot = false;
                StartCoroutine(Action.DelayedAction(shootDelay, () => _canShoot = true));
            }
        }
        
    }
}
