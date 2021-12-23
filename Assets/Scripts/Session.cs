using System;
using System.Data;
using System.Data.SQLite;

public class Session
{
    private readonly int _id;
    private readonly SQLiteConnection _connection;

    private static Session _instance;

    public static Session Instance => _instance;

    private Session(int id, SQLiteConnection connection)
    {
        _id = id;
        _connection = connection;
    }

    public static Session CreateSession(int id, SQLiteConnection connection)
    {
        return _instance ??= new Session(id, connection);
    }

    public int AddChange(int playerStateId, string message, DateTime date)
    {
        string convertedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText =
            "INSERT INTO changes (session_id, message, player_state_id, date) values ($SessionId, $Message, $PlayerStateId, $Date)";
        command.Parameters.AddWithValue("$SessionId", _id);
        command.Parameters.AddWithValue("$PlayerStateId", playerStateId);
        command.Parameters.AddWithValue("$Message", message);
        command.Parameters.AddWithValue("$Date", convertedDate);
        command.ExecuteNonQuery();

        return (int) _connection.LastInsertRowId;
    }

    public int AddNewState(PlayerData data)
    {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "INSERT INTO player_state (score, current_weapon_id) VALUES ($Score, $CurrentWeaponId)";
        command.Parameters.AddWithValue("$Score", data.Score);
        command.Parameters.AddWithValue("$CurrentWeaponId", data.CurrentWeaponId);
        command.ExecuteNonQuery();

        var stateId = (int) _connection.LastInsertRowId;

        foreach (var levelId in data.UnlockedLevels)
        {
            command.CommandText = "INSERT INTO unlocked_levels (level_id, player_state_id) VALUES ($LevelId, $PlayerStateId);";
            command.Parameters.AddWithValue("$LevelId", levelId);
            command.Parameters.AddWithValue("$PlayerStateId", stateId);
            command.ExecuteNonQuery();
        }
        
        foreach (var weaponId in data.WeaponsId)
        {
            command.CommandText = "INSERT INTO unlocked_weapons (weapon_id, player_state_id) VALUES ($WeaponId, $PlayerStateId);";
            command.Parameters.AddWithValue("$WeaponId", weaponId);
            command.Parameters.AddWithValue("$PlayerStateId", stateId);
            command.ExecuteNonQuery(); 
        }

        return stateId;
    }

    public async void RemoveIfChangesEmpty()
    {
        SQLiteCommand command = _connection.CreateCommand();

        var query = "SELECT * FROM changes WHERE session_id = @id";

        var cmd = new SQLiteCommand(query, _connection);

        cmd.Parameters.AddWithValue("@id", _id);

        int rowCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());

        if (rowCount == 0)
        {
            command.CommandText = "DELETE FROM sessions WHERE id = @id";
            command.Parameters.AddWithValue("@id", _id);
            
            await command.ExecuteNonQueryAsync();
        }
    }
}
