using System.Collections.Generic;
using Player.Weapons.Base;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Transform weaponsContainer;
        [SerializeField] private new Camera camera;
        [SerializeField] private Transform poolsContainer;
        [SerializeField] private WeaponCreator weaponCreator;
        
        private List<Weapon> _weapons;
        private Weapon _currentWeapon;
        private int _currentIndex = -1;

        public List<Weapon> Weapons => _weapons;

        private void Start()
        {
            PlayerData playerData = PlayerData.Instance;
            _weapons = new List<Weapon>();
            
            foreach (var weaponId in playerData.WeaponsId)
            {
                var weapon = weaponCreator.CreateWeapon(weaponId);
                AddWeapon(weapon).gameObject.SetActive(false);
            }
                
            ChangeWeapon(_weapons[playerData.CurrentWeaponId]);
        }

        public Weapon AddWeapon(Weapon weapon)
        {
            var boughtWeapon = Instantiate(weapon, weaponsContainer);
            boughtWeapon.Init(camera, poolsContainer);
            _weapons.Add(boughtWeapon);

            return boughtWeapon;
        }

        public void TryMoveNext()
        {
            if(_currentIndex + 1 == _weapons.Count) return;
            ChangeWeapon(_weapons[_currentIndex + 1]);
        }
        
        public void TryMoveBack()
        {
            if(_currentIndex -1 < 0) return;
            ChangeWeapon(_weapons[_currentIndex - 1]);
        }
        
        private void Update()
        {
            if (_currentWeapon.IsSingle && Input.GetMouseButtonDown(0))
            {
                _currentWeapon.Shoot();
                return;
            }
            
            if (!_currentWeapon.IsSingle && Input.GetMouseButton(0))
            {
                _currentWeapon.Shoot();
            }
        }

        public void ChangeWeapon(Weapon weapon)
        {
            if(_currentWeapon != null)
                _currentWeapon.gameObject.SetActive(false);
            
            _currentWeapon = weapon;
            _currentWeapon.gameObject.SetActive(true);
            _currentIndex = _weapons.IndexOf(_currentWeapon);
            PlayerData.Instance.CurrentWeaponId = _currentIndex;
        }
    }
}
