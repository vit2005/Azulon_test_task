using System;
using System.Collections;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class OnlinePlayerStorage : IPlayerStorage
{
    private readonly string _loadUrl;
    private readonly string _saveUrl;
    private readonly MonoBehaviour _runner;

    private static readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto
    };

    public OnlinePlayerStorage(string loadUrl, string saveUrl, MonoBehaviour runner)
    {
        _loadUrl = loadUrl;
        _saveUrl = saveUrl;
        _runner = runner;
    }

    public void Load(Action<PlayerData> onSuccess, Action<string> onError)
    {
        _runner.StartCoroutine(LoadCoroutine(onSuccess, onError));
    }

    public void Save(PlayerData data, Action onSuccess = null, Action<string> onError = null)
    {
        _runner.StartCoroutine(SaveCoroutine(data, onSuccess, onError));
    }

    private IEnumerator LoadCoroutine(Action<PlayerData> onSuccess, Action<string> onError)
    {
        using var request = UnityWebRequest.Get(_loadUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load player data: {request.error}");
            onError?.Invoke(request.error);
            yield break;
        }

        try
        {
            var data = JsonConvert.DeserializeObject<PlayerData>(request.downloadHandler.text, _settings);
            onSuccess?.Invoke(data ?? new PlayerData());
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to parse player data: {e.Message}");
            onError?.Invoke(e.Message);
        }
    }

    private IEnumerator SaveCoroutine(PlayerData data, Action onSuccess, Action<string> onError)
    {
        var json = JsonConvert.SerializeObject(data, _settings);
        var bytes = Encoding.UTF8.GetBytes(json);

        using var request = new UnityWebRequest(_saveUrl, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(bytes),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to save player data: {request.error}");
            onError?.Invoke(request.error);
            yield break;
        }

        onSuccess?.Invoke();
    }
}