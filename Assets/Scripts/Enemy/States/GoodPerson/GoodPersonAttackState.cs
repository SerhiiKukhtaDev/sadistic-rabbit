using System;
using System.Collections;
using Enemy.MeleeWeapons.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.States.GoodPerson
{
    [RequireComponent(typeof(Animator))]
    public class GoodPersonAttackState : EventState
    {
        [SerializeField] private MeleeWeapon meleeWeapon;
        [SerializeField] private int attacksPerSecond;

        private float _oneAttackTime;

        private void Awake()
        {
            _oneAttackTime = 1 / (float)attacksPerSecond;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            DoAttack();
        }

        protected override void OnDisable()
        {
            StopCoroutine(nameof(Attack));
            base.OnDisable();
        }

        void DoAttack()
        {
            meleeWeapon.Hit(Target.gameObject, _oneAttackTime);
            StartCoroutine(nameof(Attack));
        }

        IEnumerator Attack()
        {
            yield return new WaitForSeconds(_oneAttackTime);
            DoAttack();
        }
    }
}
