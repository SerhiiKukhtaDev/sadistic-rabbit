using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.States.GoodPerson
{
    [RequireComponent(typeof(EnemyFacing))]
    public class GoodPersonIdleState : EventState
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayDistance;
        [SerializeField] private float speed;
        [Range(0, 1)] [SerializeField] private float speedOffset;
        
        private EnemyFacing _enemyFacing;
        private static readonly int IsAngry = Animator.StringToHash("IsAngry");

        protected override void OnEnable()
        {
            base.OnEnable();
            GetComponent<Animator>().SetBool(IsAngry, false);
        }

        private void Start()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
            speed = Random.Range(speed - speedOffset, speed);
        }

        private void Update()
        {
            transform.Translate(Vector2.right * (Time.deltaTime * speed));
        }
        
        private void FixedUpdate()
        {
            var result = Physics2D.Raycast(transform.position, 
                transform.right, rayDistance, layerMask);

            if (result.collider != null)
            {
                _enemyFacing.Flip();
            }
        }
    }
}
