using System;
using UnityEngine;
using Weapon = Enemy.Weapons.Base.Weapon;

namespace Enemy.States.FlyingEnemy.Attack
{
    [RequireComponent(typeof(EnemyFacing))]
    public class SniperAttackState : EventState
    {
        [SerializeField] private Weapon weapon;

        private EnemyFacing _enemyFacing;
        
        private void Awake()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
        }

        protected override void OnEnable()
        {
            _enemyFacing.SetAutoCheck(Target);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _enemyFacing.DisableAutoCheck();
            
            base.OnDisable();
        }

        private void Update()
        {
            weapon.TryShoot();
        }
    }
}
