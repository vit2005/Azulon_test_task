using System;

public interface IPlayerStorage
{
    void Load(Action<PlayerData> onSuccess, Action<string> onError);
    void Save(PlayerData data, Action onSuccess = null, Action<string> onError = null);
}