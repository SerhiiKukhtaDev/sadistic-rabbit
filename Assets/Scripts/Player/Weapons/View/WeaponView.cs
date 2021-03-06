using System;
using Interfaces;
using Player.Weapons.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Player.Weapons.View
{
    public class WeaponView : MonoBehaviour, IView, IRenderedDynamic
    {
        [SerializeField] private Text label;
        [SerializeField] private Image image;
        [SerializeField] private Text price;
        [SerializeField] private Button buyButton;

        public static UnityAction<Weapon> SellButtonClick { get; set; }

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

        public void Render(IRenderView template)
        {
            Weapon weapon = (Weapon)template;
            _weapon = weapon;
            
            label.text = weapon.Name;
            image.sprite = weapon.Sprite;
            price.text = weapon.Price.ToString();
            
            TryLockItem();
        }

        private void ButtonClick()
        {
            SellButtonClick?.Invoke(_weapon);
        }
    }
}
