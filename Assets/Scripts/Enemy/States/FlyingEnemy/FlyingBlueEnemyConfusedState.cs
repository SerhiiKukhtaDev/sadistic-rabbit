using UnityEngine;

namespace Enemy.States.FlyingEnemy
{
    [RequireComponent(typeof(EnemyFacing))]
    public class FlyingBlueEnemyConfusedState : EventState
    {
        private EnemyFacing _enemyFacing;
        
        private void Awake()
        {
            _enemyFacing = GetComponent<EnemyFacing>();
        }

        protected override void OnEnable()
        {
            _enemyFacing.DisableAutoCheck();
            _enemyFacing.TryFlipByTargetDistance(Target.Position);
            
            base.OnEnable();
        }
    }
}
