using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Award
{
    public class AwardSpawner : MonoBehaviour
    {
        [SerializeField] private List<PooledAward> pooledAwards;
        [SerializeField] private float spawnOffset;
        [SerializeField] private LayerMask layerMask;
        private List<Award> _awards;

        private void Start()
        {
            _awards = new List<Award>();
            
            foreach (var pooledAward in pooledAwards)
            {
                pooledAward.Award.Pool = pooledAward.Pool;
                _awards.Add(pooledAward.Award);
            }
        }

        public void Spawn(Vector3 position, int awardValue)
        {
            List<Award> awardsToSpawn = CalculateAward(_awards, awardValue);
            
            foreach (var award in awardsToSpawn)
            {
                var spawnPos = GetPositionWithOffset(position, spawnOffset);
                var elem = award.Pool.GetFreeElement(spawnPos);
            }
        }

        private Vector3 GetPositionWithOffset(Vector3 initialPosition, float distance)
        {
            var newPosition = new Vector3(initialPosition.x + Random.Range(-distance, distance), initialPosition.y);
            var direction = (newPosition - initialPosition).normalized;
            RaycastHit2D result = Physics2D.Raycast(initialPosition, direction, distance, layerMask);

            return result.collider == null ? newPosition : initialPosition;
        }

        private List<Award> CalculateAward(List<Award> awards, int awardValue)
        {
            int[] moneyCount = new int[awards.Count];

            List<Award> results = new List<Award>();

            for (int i = awards.Count-1; awardValue > 0 && i >= 0; i--) {
                moneyCount[i] = (awardValue/awards[i].Value);

                for (int j = 0; j < moneyCount[i]; j++)
                {
                    results.Add(awards[i]);
                }
                
                awardValue -= moneyCount[i] * awards[i].Value;            
            }

            return results;
        }
    }

    [Serializable]
    public class PooledAward
    {
        [SerializeField] private Award award;
        [SerializeField] private ObjectPool pool;

        public Award Award => award;
        public ObjectPool Pool => pool;
    }
}
