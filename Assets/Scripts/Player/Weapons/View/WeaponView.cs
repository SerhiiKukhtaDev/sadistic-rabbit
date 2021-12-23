using System;
using Interfaces;
using Player.Weapons.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Player.Weapons.View
{
    public class WeaponView : MonoBehaviour, IView, IRenderedDynamic<Weapon>
    {
        [SerializeField] private Text label;
        [SerializeField] private Image image;
        [SerializeField] private Text price;
        [SerializeField] private Button buyButton;

        public UnityAction<Weapon, WeaponView> OnSellButtonClick { get; set; }

        private Weapon _weapon;

        private void OnEnable()
        {
            buyButton.onClick.AddListener(ButtonClick);
            buyButton.onClick.AddListener(TryLockItem);
        }

        private void OnDisable()
        {
            buyButton.onClick.RemoveListener(ButtonClick);
            buyButton.onClick.RemoveListener(TryLockItem);
        }

        private void TryLockItem()
        {
            if (_weapon.IsBought || PlayerData.Instance.WeaponsId.Contains(_weapon.ID))
                LockItem();
        }

        private void LockItem()
        {
            buyButton.interactable = false;
        }

        public void Render(Weapon weapon)
        {
            _weapon = weapon;
            
            label.text = weapon.Name;
            image.sprite = weapon.Sprite;
            price.text = weapon.Price.ToString();
            TryLockItem();
        }

        private void ButtonClick()
        {
            OnSellButtonClick?.Invoke(_weapon, this);
        }
    }
}
