using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Panels
{
    public class FinishLevelPanel : MonoBehaviour
    {
        [SerializeField] private UnityEvent appeared;
        [SerializeField] private UnityEvent exit;

        private void OnEnable()
        {
            GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).OnComplete(() =>
            {
                appeared?.Invoke();   
            });
        }

        private void OnDisable()
        {
            exit?.Invoke();
        }
    }
}
