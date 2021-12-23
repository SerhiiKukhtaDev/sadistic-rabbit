using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<int> UnlockedLevels { get; set; }

    public List<int> WeaponsId { get; set; }
    
    public int CurrentWeaponId { get; set; }
    
    public float Score { get; set; }

    private static PlayerData _instance;
    
    public static PlayerData Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = new PlayerData()
            {
                UnlockedLevels = new List<int>{0},
                WeaponsId = new List<int> {0},
                CurrentWeaponId = 0,
                Score = 15000
            };
            
            return _instance;
        }
    }

    public void SetData(PlayerStateEntity playerState)
    {
        Score = playerState.Score;
        CurrentWeaponId = playerState.CurrentWeaponId;
        WeaponsId = playerState.UnlockedWeapons;
        UnlockedLevels = playerState.UnlockedLevels;
    }
}
