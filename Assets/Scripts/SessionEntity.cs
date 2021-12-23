using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SessionEntity
{
    private int _id;
    private DateTime _date;
    

    private List<ChangeEntity> _changes;
    
    public DateTime Date => _date;

    public List<ChangeEntity> Changes => new List<ChangeEntity>(_changes);

    public int ID => _id;

    public SessionEntity(int id, DateTime date)
    {
        _date = date;
        _id = id;
        _changes = new List<ChangeEntity>();
    }

    public void AddChange(ChangeEntity changeEntity)
    {
        _changes.Add(changeEntity);
    }
}
