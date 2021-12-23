using System.Collections;
using Player;
using UnityEngine;

namespace Award
{
    [RequireComponent(typeof(PooledObject))]
    public class Award : MonoBehaviour
    {
        [SerializeField] protected int value;
        [SerializeField] private float animationTime;
        
        private Animator _animator;
        private Transform _transform;
        private bool _isEntered;
        private Vector3 _startScale;
        
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _isEntered = false;
        }

        public ObjectPool Pool { get; set; }

        public int Value => value;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerBase playerBase) && !_isEntered)
            {
                _isEntered = true;
                playerBase.ApplyScore(value);
                GetComponent<PooledObject>().ReturnToPool();
            }
        }
        
        public void DoFade()
        {
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            float elapsedTime = 0;
        
            while (elapsedTime <= animationTime)
            {
                var value = Mathf.Lerp(1, 0, elapsedTime / animationTime);
                _transform.localScale = new Vector3(value, value);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _transform.localScale = Vector2.zero;
            
            GetComponent<PooledObject>().ReturnToPool();
        }
    }
}
