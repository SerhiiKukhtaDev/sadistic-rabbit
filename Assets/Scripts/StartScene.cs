using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
            string path = Application.persistentDataPath + "/sessions.db";
            
            if (!File.Exists(path))
            {
                SessionsDatabase.CreateConnection(path);
                SessionsDatabase.Create();
                
                var session = SessionsDatabase.CreateSession();
                var stateId = session.AddNewState(PlayerData.Instance);
                session.AddChange(stateId, $"Start game", DateTime.Now);
            }
            else
            {
                SessionsDatabase.CreateConnection(path);
                SessionsDatabase.CreateSession();
            }
            
            var lastChangeId = SessionsDatabase.GetLastChangeId();
            var data = SessionsDatabase.GetPlayerInfo(lastChangeId);
            
            PlayerData.Instance.SetData(data);
    }

    private void OnApplicationQuit()
    {
       Session.Instance.RemoveIfChangesEmpty();
       SessionsDatabase.CloseConnection();
    }
}
