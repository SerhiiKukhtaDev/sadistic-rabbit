using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int YVelocity = Animator.StringToHash("YVelocity");

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetJumpAnimation(float yVelocity)
        {
            _animator.SetFloat(YVelocity, yVelocity);
        }
    }
}
