using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public float HorizontalAxis { get; private set; }
        
        public float HorizontalAxisRaw { get; private set; }
    
        public float VerticalAxis { get; private set; }
        
        public bool SpacePressed { get; private set; }
    
        private void Update()
        {
            HorizontalAxis = Input.GetAxis("Horizontal");
            HorizontalAxisRaw = Input.GetAxisRaw("Horizontal");
            VerticalAxis = Input.GetAxis("Vertical");
            SpacePressed = Input.GetKey(KeyCode.Space);
        }
    }
}
