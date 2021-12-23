using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Player.Weapons.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Panels
{
    public class NotifyPanel : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private RectTransform firstBlock;
        [SerializeField] private RectTransform secondBlock;
        [SerializeField] private float animationTime;
        [SerializeField] private float showTime;
        [SerializeField] private float delay;
        
        private Vector2 _firstBlockStartPosition;
        private Vector2 _secondBlockStartPosition;

        private void Start()
        {
            _firstBlockStartPosition = firstBlock.anchoredPosition;
            _secondBlockStartPosition = secondBlock.anchoredPosition;
        }

        public void OnSuccessfulBuyOneWeapon(Weapon weapon)
        {
            NotifyWith($"Bought {weapon.Name} for {weapon.Price}");
        }
        
        public void OnSuccessfulBuySeveralWeapons(List<Weapon> weapons)
        {
            NotifyWith($"Bought {weapons.Count} weapons for {weapons.Sum(w => w.Price)}");
        }

        public void AllEnemiesDie()
        {
            NotifyWith($"All enemies die!");
        }

        private void NotifyWith(string message)
        {
            var firstTween = firstBlock.DOAnchorPos(Vector2.zero, animationTime)
                .SetEase(Ease.Flash).SetDelay(delay);
            var secondTween = secondBlock.DOAnchorPos(Vector2.zero, animationTime).SetAs(firstTween);

            secondTween.OnComplete(() =>
            {
                text.text = message;
                var returningTween = firstBlock.DOAnchorPos(_firstBlockStartPosition, animationTime)
                    .SetDelay(showTime);
                
                secondBlock.DOAnchorPos(_secondBlockStartPosition, animationTime).SetAs(returningTween).OnStart(() =>
                {
                    text.text = string.Empty;
                });
            });
        }
    }
}
