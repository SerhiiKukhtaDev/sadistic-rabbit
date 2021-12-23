using UnityEngine;

namespace Player
{
    public class MouseAngleChecker : MonoBehaviour
    {
        private Camera _camera;
    
        private void Update()
        {
            Vector3 direction = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, z);
        }

        public void Init(Camera camera)
        {
            _camera = camera;
        }
    }
}
