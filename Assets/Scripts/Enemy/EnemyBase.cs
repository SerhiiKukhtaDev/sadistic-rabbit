using Award;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private UnityEvent<float> onDamageTaken;
        [SerializeField] private int award;
        [SerializeField] private AwardSpawner awardSpawner;

        public UnityAction<EnemyBase> Die { get; set; }

        private float _takenDamage;
        private float _startHealth;

        private void Start()
        {
            _startHealth = health;
        }

        public virtual void ApplyDamage(float damage)
        {
            float appliedHealth = Mathf.Clamp(health - damage, 0, health);
            _takenDamage += damage;
            
            onDamageTaken.Invoke(1 - (_takenDamage / _startHealth));
            health = appliedHealth;

            if (health <= 0)
            {
                Death();
            }
        }

        public virtual void Death()
        {
            Die?.Invoke(this);
            awardSpawner.Spawn(transform.position, award);
            Destroy(gameObject);
        }
    }
}
