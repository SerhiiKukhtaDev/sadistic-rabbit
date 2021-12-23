using System;
using System.Linq;
using Player.Weapons.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(PlayerShoot))]
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private UnityEvent<float> onDamageTaken;
        [SerializeField] private UnityEvent<float> scoreChanged;
        [SerializeField] private UnityEvent onPlayerDie;

        private PlayerShoot _playerShoot;
        private float _takenDamage;
        private float _startHealth;
        private float _score = 10000;
        private Animator _animator;
        
        private static readonly int PlayerAttacked = Animator.StringToHash("PlayerAttacked");

        public float Score => _score;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _startHealth = health;
            _playerShoot = GetComponent<PlayerShoot>();
            _score = PlayerData.Instance.Score;
            scoreChanged?.Invoke(_score);
        }

        public void ApplyScore(float score)
        {
            _score += score;
            scoreChanged?.Invoke(_score);
            PlayerData.Instance.Score = _score;
        }

        public void ApplyDamage(float damage)
        {
            float appliedHealth = Mathf.Clamp(health - damage, 0, health);
            _takenDamage += damage;
            
            onDamageTaken.Invoke(1 - (_takenDamage / _startHealth));
            _animator.SetTrigger(PlayerAttacked);

            health = appliedHealth;

            if (health <= 0)
            {
                Death();
            }
        }

        public void BuyWeapon(Weapon weapon)
        {
            var addedWeapon = _playerShoot.AddWeapon(weapon);
            _playerShoot.ChangeWeapon(addedWeapon);
            
            var data = PlayerData.Instance;

            ApplyScore(-weapon.Price);
            data.WeaponsId.Add(addedWeapon.ID);

            string message = $"Bought weapon {weapon.Name} for {weapon.Price}";
            var stateId = Session.Instance.AddNewState(PlayerData.Instance);
            Session.Instance.AddChange(stateId, message, DateTime.Now);
        }

        public void Death()
        {
            onPlayerDie.Invoke();
            Destroy(gameObject);
        }
    }
}
