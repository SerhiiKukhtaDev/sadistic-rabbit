using System.Collections.Generic;
using Player.Weapons.Base;
using UnityEngine;

public class WeaponCreator : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;

    public Weapon CreateWeapon(int weaponId)
    {
        return weapons.Find(weapon => weapon.ID == weaponId);
    }
}
