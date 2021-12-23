using System.Collections;
using Player.Weapons.Base;
using UnityEngine;

namespace Player.Weapons
{
    public class Shotgun : Weapon
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootDelay;
        
        private bool _canShoot = true;

        private void OnEnable()
        {
            _canShoot = true;
        }

        public override void Shoot()
        {
            if(!_canShoot) return;
            
            DoShoot();
            _canShoot = false;
            
            StartCoroutine(Reload());
        }

        private void DoShoot()
        {
            var rotation = shootPoint.eulerAngles;
            var position = shootPoint.position;
            
            var firstPoint = Quaternion.Euler(rotation.x, rotation.y,rotation.z + 5);
            var secondPoint = Quaternion.Euler(rotation.x, rotation.y,rotation.z - 5);
            
            Pool.GetFreeElement(position, firstPoint);
            Pool.GetFreeElement(position, shootPoint.rotation);
            Pool.GetFreeElement(position, secondPoint);
        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(shootDelay);
            _canShoot = true;
        }
    }
}
