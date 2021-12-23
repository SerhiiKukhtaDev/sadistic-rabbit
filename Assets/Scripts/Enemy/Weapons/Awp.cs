using System;
using System.Collections;
using Enemy.Weapons.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.Weapons
{
    public class Awp : Weapon
    {
        [SerializeField] private float timeBetweenAttacks;
        [SerializeField] private UnityEvent<float> timeBetweenAttacksChanged;
        
        private bool _canShoot = true;
        private float _elapsedTime;
        
        private void OnEnable()
        {
            StartCoroutine(Reload());
        }

        public override void TryShoot()
        {
            if (!_canShoot) return;
            
            Shoot();
            StartCoroutine(nameof(Reload));
        }

        IEnumerator Reload()
        {
            _canShoot = false;
            _elapsedTime = 0;
            
            while (_elapsedTime <= timeBetweenAttacks)
            {
                _elapsedTime += Time.deltaTime;
                timeBetweenAttacksChanged?.Invoke(_elapsedTime / timeBetweenAttacks);
                yield return null;
            }

            _canShoot = true;
        }
        
        public void Shoot()
        {
            ObjectPool.GetFreeElement(shootPoint.position, transform.rotation);
        }
    }
}
