using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(PlayerBase))]
    public class PlayerSwim : MonoBehaviour
    {
        [SerializeField] private float timeToApplyingDamage;
        [SerializeField] private float damage;
        [SerializeField] private UnityEvent<float> timeToApplyingDamageChanged;

        [FormerlySerializedAs("OnWaterCollision")] [SerializeField] private UnityEvent<bool> onWaterCollision;

        private bool _isEntered;
        private float _elapsedTime;
        private PlayerBase _playerBase;

        private void Start()
        {
            _playerBase = GetComponent<PlayerBase>();
        }

        private void Update()
        {
            if (_isEntered)
            {
                _elapsedTime += Time.deltaTime;
            
                timeToApplyingDamageChanged.Invoke( 1 - (_elapsedTime / timeToApplyingDamage));

                if (!(_elapsedTime >= timeToApplyingDamage)) return;
            
                _playerBase.ApplyDamage(damage);
                _elapsedTime = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Water _))
            {
                _isEntered = true;
                onWaterCollision.Invoke(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Water _))
            {
                _isEntered = false;
                _elapsedTime = 0;
                timeToApplyingDamageChanged.Invoke(1);
                onWaterCollision.Invoke(false);
            }
        }
    }
}
