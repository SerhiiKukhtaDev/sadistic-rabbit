using System.Collections;
using UnityEngine;
using Weapon = Player.Weapons.Base.Weapon;

namespace Player.Weapons
{
    public class Pistol : Weapon
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
            if (_canShoot)
            {
                Pool.GetFreeElement(shootPoint.position, shootPoint.rotation);
                _canShoot = false;
                StartCoroutine(Action.DelayedAction(shootDelay, () => _canShoot = true));
            }
        }

        IEnumerator Refresh()
        {
            yield return new WaitForSeconds(shootDelay);
            _canShoot = true;
        }
    }
}
