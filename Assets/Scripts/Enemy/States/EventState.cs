using System;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.States
{
    public class EventState : State
    {
        [SerializeField] private UnityEvent onEnable;
        [SerializeField] private UnityEvent onDisable;
        protected virtual void OnEnable()
        {
            onEnable?.Invoke();
        }

        protected virtual void OnDisable()
        {
            onDisable?.Invoke();
        }
    }
}
