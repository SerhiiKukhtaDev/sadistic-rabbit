using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerFlip : MonoBehaviour
    {
        [SerializeField] private bool facingRight = true;
        [SerializeField] private UnityEvent onFlip;
        [SerializeField] private new Camera camera;
        [SerializeField] private Transform relativePoint;

        private Vector3 _theScale;

        private void Start()
        {
            _theScale = GetComponent<Transform>().localScale;
        }

        void Update()
        {
            
            var screenPos = camera.WorldToScreenPoint(relativePoint.position);
            if(Input.mousePosition.x > screenPos.x && !facingRight) Flip(); 
            else if(Input.mousePosition.x < screenPos.x && facingRight) Flip();
        }
    
        void Flip()
        {
            facingRight = !facingRight;
            
            _theScale.x *= -1;
            transform.localScale = _theScale;
            onFlip?.Invoke();
        }
    }
}
