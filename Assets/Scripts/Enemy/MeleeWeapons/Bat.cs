using System;
using System.Collections;
using Enemy.MeleeWeapons.Base;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.MeleeWeapons
{
    public class Bat : MeleeWeapon
    {
        [SerializeField] private float startRotation;
        [SerializeField] private float endRotation;
        
        public override void Hit(GameObject target, float attackTime)
        {
            StartCoroutine(Attack(startRotation, endRotation, attackTime, () =>
            {
                target.GetComponent<PlayerBase>().ApplyDamage(damage);
            }));
        }

        IEnumerator Attack(float startValue, float endValue, float attackTime, UnityAction onCompleted = null)
        {
            float elapsedTime = 0;

            while (elapsedTime <= attackTime)
            {
                elapsedTime += Time.deltaTime;
                float z = Mathf.Lerp(startValue, endValue, elapsedTime / attackTime);
                transform.rotation = Quaternion.Euler(0, 0, z);
                yield return null;
            }
            
            onCompleted?.Invoke();
        }
    }
}
