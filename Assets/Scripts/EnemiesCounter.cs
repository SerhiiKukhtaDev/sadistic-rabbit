using System;
using System.Linq;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> numberOfEnemiesChanged;
    [SerializeField] private UnityEvent allEnemiesDie;
    
    private EnemyBase[] _enemies;
    private int _startValue;
    
    private void Start()
    {
        _enemies = GetComponentsInChildren<EnemyBase>();
        _startValue = _enemies.Length;
        
        foreach (var enemyBase in _enemies)
        {
            enemyBase.Die += OnEnemyDie;
        }
    }

    private void OnEnemyDie(EnemyBase enemyBase)
    {
        RemoveEnemy(enemyBase);
        var count = _enemies.Length;
        
        numberOfEnemiesChanged.Invoke( 1 - (float)(_startValue - count) / _startValue);
        
        if(count == 0)
            allEnemiesDie?.Invoke();
    }

    private void RemoveEnemy(EnemyBase enemyBase)
    {
        _enemies = (from enemy in _enemies where !enemy.Equals(enemyBase) select enemy).ToArray();
    }
}
