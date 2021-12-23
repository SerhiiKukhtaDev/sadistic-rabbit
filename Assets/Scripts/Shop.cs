using System.Collections.Generic;
using Comparers;
using Player;
using Player.Weapons.Base;
using Player.Weapons.View;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private WeaponView template;
    [SerializeField] private GameObject container;
    [SerializeField] private PlayerBase player;
    [SerializeField] private bool hideOnBuy;

    [SerializeField] private UnityEvent shopEntered;
    [SerializeField] private UnityEvent shopExit;
    [SerializeField] private UnityEvent<Weapon> successfulBuyOneWeapon;
    [SerializeField] private UnityEvent<List<Weapon>> successfulBuySeveralWeapons;
    
    private List<Weapon> _boughtWeapons;

    private void Start()
    {
        weapons.Sort(new WeaponComparer());
        weapons.ForEach(AddToShop);
        
        _boughtWeapons = new List<Weapon>();
    }

    private void OnEnable()
    {
        shopEntered?.Invoke();
        shopExit.AddListener(OnExit);
    }

    private void OnDisable()
    {
        shopExit?.Invoke();
    }

    private void AddToShop(Weapon weapon)
    {
        WeaponView weaponView = Instantiate(template, container.transform);
        weaponView.Render(weapon);
        
        weaponView.OnSellButtonClick += TrySellWeapon;
    }

    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if(player.Score < weapon.Price) return;
        
        player.BuyWeapon(weapon);
        weapon.Buy();
        
        _boughtWeapons.Add(weapon);
        
        view.OnSellButtonClick -= TrySellWeapon;

        if (hideOnBuy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnExit()
    {
        if(_boughtWeapons.Count == 0) return;

        if(_boughtWeapons.Count == 1)
            successfulBuyOneWeapon?.Invoke(_boughtWeapons[0]);
        else if(_boughtWeapons.Count > 1) 
            successfulBuySeveralWeapons?.Invoke(_boughtWeapons);
        
        _boughtWeapons.Clear();
    }
}
