using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class OfflinePlayerStorage : IPlayerStorage
{
    private readonly string _filePath;

    private static readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto, // потрібно для Dictionary та спадкування
        Formatting = Newtonsoft.Json.Formatting.Indented
    };

    public OfflinePlayerStorage(string fileName = "player_data.json")
    {
        _filePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void Load(Action<PlayerData> onSuccess, Action<string> onError)
    {
        if (!File.Exists(_filePath))
        {
            var currencies = new Dictionary<CurrencyType, float>
            {
                { CurrencyType.Gold, 0 },
                { CurrencyType.Gems, 100 }
            };

            var initialData = new PlayerData
            {
                currencies = currencies,
                rebirths = 0,
                studioData = new StudioData() { programmers = new List<ProgrammerItem>() },
                inventoryData = new InventoryData() { programmers = new List<ProgrammerItem>() }
            };

            var json = JsonConvert.SerializeObject(initialData, _settings);
            File.WriteAllText(_filePath, json);
            onSuccess?.Invoke(new PlayerData());
            return;
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonConvert.DeserializeObject<PlayerData>(json, _settings);
            onSuccess?.Invoke(data ?? new PlayerData());
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load player data: {e.Message}");
            onError?.Invoke(e.Message);
        }
    }

    public void Save(PlayerData data, Action onSuccess = null, Action<string> onError = null)
    {
        try
        {
            var json = JsonConvert.SerializeObject(data, _settings);
            File.WriteAllText(_filePath, json);
            onSuccess?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save player data: {e.Message}");
            onError?.Invoke(e.Message);
        }
    }
}