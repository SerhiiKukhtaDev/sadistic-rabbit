using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

public static class SessionsDatabase
{
    private static SQLiteConnection _connection;
    
    public static Session CreateSession()
    {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "INSERT INTO sessions (date) VALUES (datetime('now', 'localtime'))";
        command.ExecuteNonQuery();

        return Session.CreateSession((int)_connection.LastInsertRowId, _connection);
    }

    public static void DeleteAllChangesAbove(int changeId)
    {
        var pragma = new SQLiteCommand( "PRAGMA foreign_keys = true;", _connection );
            pragma.ExecuteNonQuery();
    
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "DELETE FROM player_state WHERE id IN (SELECT player_state_id from changes where changes.id > $id)";
        command.Parameters.AddWithValue("$id", changeId);
        command.ExecuteNonQuery();
    }

    public static void CreateConnection(string path)
    {
        string str = "URI=file:" + path;
        _connection = new SQLiteConnection(str);
        _connection.Open();
    }

    public static void CloseConnection()
    {
        _connection.Close();
    }
    
    public static void Create()
    {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandType = CommandType.Text;

        command.CommandText = "CREATE TABLE IF NOT EXISTS 'sessions' ( " +
                              "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                              "  'date' Date " +
                              ");" +
                              "CREATE TABLE IF NOT EXISTS 'player_state' ( " +
                              "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                              "  'score' INTEGER, " +
                              "  'current_weapon_id' INTEGER" +
                              ");"  +
                              "CREATE TABLE IF NOT EXISTS 'changes' ( " +
                              "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                              "  'session_id' INTEGER REFERENCES sessions (id) ON DELETE CASCADE, " +
                              "  'message' STRING," +
                              "  'player_state_id' INTEGER REFERENCES player_state(id) ON DELETE CASCADE, " +
                              "  'date' Date" +
                              ");"  +
                              "CREATE TABLE IF NOT EXISTS 'unlocked_levels' ( " +
                              "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                              "  'level_id' INTEGER, " +
                              "  'player_state_id' INTEGER REFERENCES player_state (id) ON DELETE CASCADE" +
                              ");"+
                              "CREATE TABLE IF NOT EXISTS 'unlocked_weapons' ( " +
                              "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                              "  'weapon_id' INTEGER, " +
                              "  'player_state_id' INTEGER REFERENCES player_state (id) ON DELETE CASCADE" +
                              ");";

        
        command.ExecuteNonQuery();
    }

    public static int GetLastChangeId()
    {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT id FROM changes ORDER BY id DESC LIMIT 1";
        int value = 0;
        
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            value = Convert.ToInt32(reader.GetValue(0));
        }

        return value;
    }

    public static PlayerStateEntity GetPlayerInfo(int changeId)
    {
        var levels = new List<int>();
        var weapons = new List<int>();
        int playerScore = 0, currentWeapon = 0;
        
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT player_state.score, player_state.current_weapon_id, unlocked_levels.level_id, unlocked_weapons.weapon_id FROM ((player_state INNER JOIN changes ON player_state.id = changes.player_state_id) INNER JOIN unlocked_levels ON player_state.id = unlocked_levels.player_state_id) INNER JOIN unlocked_weapons ON player_state.id = unlocked_weapons.player_state_id WHERE (((changes.id)=@id))";
        command.Parameters.AddWithValue("@id", changeId);

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                playerScore = Convert.ToInt32(reader.GetValue(0));
                currentWeapon = Convert.ToInt32(reader.GetValue(1));
                var levelId = Convert.ToInt32(reader.GetValue(2));
                if(!levels.Contains(levelId)) levels.Add(levelId);
                var weaponId = Convert.ToInt32(reader.GetValue(3));
                if(!weapons.Contains(weaponId)) weapons.Add(weaponId);
            }
        }

        return new PlayerStateEntity(playerScore, currentWeapon, levels, weapons);
    }

    public static List<SessionEntity> ReadAllData()
    {
        var sessions = new List<SessionEntity>();
        var playerStates = new List<PlayerStateEntity>();
        
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT * FROM ((sessions INNER JOIN (player_state INNER JOIN changes ON player_state.id = changes.player_state_id) ON sessions.id = changes.session_id) INNER JOIN unlocked_levels ON player_state.id = unlocked_levels.player_state_id) INNER JOIN unlocked_weapons ON player_state.id = unlocked_weapons.player_state_id";

        using (SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var sessionId = Convert.ToInt32(reader.GetValue(0));
                var sessionDate = Convert.ToDateTime(reader.GetValue(1));
                var session = sessions.FirstOrDefault(session => session.ID == sessionId);

                if (session == null)
                {
                    session = new SessionEntity(sessionId, sessionDate);
                    sessions.Add(session);
                }
               
                var playerStateId = Convert.ToInt32(reader.GetValue(2));
                var playerScore = Convert.ToInt32(reader.GetValue(3));
                var playerCurrentWeapon = Convert.ToInt32(reader.GetValue(4));
                var levelId = Convert.ToInt32(reader.GetValue(11));
                var weaponId = Convert.ToInt32(reader.GetValue(14));

                var playerStateEntity = playerStates.FirstOrDefault(playerState => playerState.ID == playerStateId);

                if (playerStateEntity == null)
                {
                    playerStateEntity = new PlayerStateEntity(playerStateId, playerScore, playerCurrentWeapon);
                    playerStates.Add(playerStateEntity);
                }

                if(!playerStateEntity.UnlockedLevels.Contains(levelId)) playerStateEntity.AddLevel(levelId);
                if(!playerStateEntity.UnlockedWeapons.Contains(weaponId)) playerStateEntity.AddWeapon(weaponId);
                
                var changeId = Convert.ToInt32(reader.GetValue(5));
                var changeMessage = Convert.ToString(reader.GetValue(7));
                var changeDate = Convert.ToDateTime(reader.GetValue(9));
                
                if(session.Changes.Exists(c => c.ID == changeId)) continue;
                
                session.AddChange(new ChangeEntity(changeId, changeMessage, changeDate, playerStateEntity));
            }
        }

        return sessions;
    }
}
