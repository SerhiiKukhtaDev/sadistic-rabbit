using System;
using Player;
using UnityEngine;

namespace Enemy.MeleeWeapons.Base
{
    public abstract class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] protected float damage;

        public abstract void Hit(GameObject target, float attackTime);
    }
}
