using System;
using Levels;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    [SerializeField] private Level currentLevel;
    [SerializeField] private Level levelToUnlock;
    [SerializeField] private UnityEvent finished;
    [SerializeField] private Color unlockedColor;

    private SpriteRenderer _spriteRenderer;
    private bool _isEntered;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CanLevelBeEnded { get; set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerBase _) && CanLevelBeEnded && !_isEntered)
        {
            _isEntered = true;
            var unlockedLevels = PlayerData.Instance.UnlockedLevels;
            int stateId;
            
            if (!unlockedLevels.Contains(levelToUnlock.ID))
            {
                unlockedLevels.Add(levelToUnlock.ID);
                
                stateId = Session.Instance.AddNewState(PlayerData.Instance);
                Session.Instance.AddChange(stateId, $"Unlocked {levelToUnlock.Name}", DateTime.Now);
            }
            else
            {
                stateId = Session.Instance.AddNewState(PlayerData.Instance);
                Session.Instance.AddChange(stateId, $"{currentLevel.Name} finished", DateTime.Now);
            }

            finished?.Invoke();
        }
    }

    public void Unlock()
    {
        CanLevelBeEnded = true;
        _spriteRenderer.color = unlockedColor;
    }
}
