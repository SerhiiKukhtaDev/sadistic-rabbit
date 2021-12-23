using UnityEngine;
using Weapon = Enemy.Weapons.Base.Weapon;

namespace Enemy.States.FlyingEnemy
{
    public class FlyingBlueEnemyAttackState : EventState
    {
        [SerializeField] private Weapon weapon;

        private void Update()
        {
            weapon.TryShoot();
        }
    }
}
