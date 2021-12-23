using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateEntity
{
    private int _id;
    private int _score;
    private int _currentWeaponId;
    private List<int> _unlockedLevels;
    private List<int> _unlockedWeapons;

    public int Score => _score;

    public int ID => _id;

    public List<int> UnlockedLevels => new List<int>(_unlockedLevels);

    public List<int> UnlockedWeapons => new List<int>(_unlockedWeapons);

    public int CurrentWeaponId => _currentWeaponId;

    public PlayerStateEntity(int id, int score, int currentWeaponId, List<int> unlockedLevels, List<int> unlockedWeapons)
    {
        _id = id;
        _score = score;
        _currentWeaponId = currentWeaponId;
        _unlockedLevels = unlockedLevels;
        _unlockedWeapons = unlockedWeapons;
    }
    
    public PlayerStateEntity(int score, int currentWeaponId, List<int> unlockedLevels, List<int> unlockedWeapons)
        : this(-1, score, currentWeaponId, unlockedLevels, unlockedWeapons) {}
    
    public PlayerStateEntity(int id, int score, int currentWeaponId)
        :this(id, score, currentWeaponId, new List<int>(), new List<int>()) {}

    public void AddWeapon(int weaponId)
    {
        _unlockedWeapons.Add(weaponId);
    }

    public void AddLevel(int levelId)
    {
        _unlockedLevels.Add(levelId);
    }
}
