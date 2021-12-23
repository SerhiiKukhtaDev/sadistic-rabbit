using System.Data.SQLite;

public class PlayerState
{
    private readonly int _id;
    private readonly SQLiteConnection _connection;
    
    public PlayerState(int id, int score, int currentWeaponID, SQLiteConnection connection)
    {
        _id = id;
        _connection = connection;
    }

    public int ID => _id;

    public void AddLevel(int levelId)
    {
        using var cmd = new SQLiteCommand(_connection);

        cmd.CommandText = "INSERT INTO unlocked_levels (level_id, player_state_id) VALUES ($LevelId, $PlayerStateId);";
        cmd.Parameters.AddWithValue("$LevelId", levelId);
        cmd.Parameters.AddWithValue("$PlayerStateId", _id);
        cmd.ExecuteNonQuery(); 
    }

    public void AddWeapon(int weaponId)
    {
        var cmd = new SQLiteCommand(_connection);
        cmd.CommandText = "INSERT INTO unlocked_weapons (weapon_id, player_state_id) VALUES ($WeaponId, $PlayerStateId);";
        cmd.Parameters.AddWithValue("$WeaponId", weaponId);
        cmd.Parameters.AddWithValue("$PlayerStateId", _id);
        cmd.ExecuteNonQuery(); 
    }
}
