using System.Collections;
using Enemy.Weapons.Base;
using UnityEngine;

namespace Enemy.Weapons
{
    public class Rifle : Weapon
    {
        [SerializeField] private float timeBetweenAttacks;

        private bool _canShoot = true;

        public override void TryShoot()
        {
            if (_canShoot)
            {
                ObjectPool.GetFreeElement(shootPoint.position, transform.rotation);
                StartCoroutine(Reload());
            }
        }

        IEnumerator Reload()
        {
            _canShoot = false;
            var waitForSeconds = new WaitForSeconds(timeBetweenAttacks);
            yield return waitForSeconds;
            _canShoot = true;
        }
    }
}
