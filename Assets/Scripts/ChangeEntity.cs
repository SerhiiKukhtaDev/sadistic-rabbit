using System;

[Serializable]
public class ChangeEntity
{
    private int _id;
    private string _message;
    private DateTime _date;
    private PlayerStateEntity _playerStateEntity;

    public DateTime Date => _date;

    public int ID => _id;

    public PlayerStateEntity PlayerStateEntity => _playerStateEntity;

    public string Message => _message;

    public ChangeEntity(int id, string message, DateTime date, PlayerStateEntity playerStateEntity)
    {
        _id = id;
        _message = message;
        _date = date;
        _playerStateEntity = playerStateEntity;
    }
}
