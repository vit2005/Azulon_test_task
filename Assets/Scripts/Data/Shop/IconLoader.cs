using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IconLoader
{
    private readonly Dictionary<string, Sprite> _cache = new();
    private readonly MonoBehaviour _runner;

    public IconLoader(MonoBehaviour runner)
    {
        _runner = runner;
    }

    public void Load(string url, Action<Sprite> onSuccess, Action<string> onError = null)
    {
        if (_cache.TryGetValue(url, out var cached))
        {
            onSuccess?.Invoke(cached);
            return;
        }

        _runner.StartCoroutine(LoadIcon(url, onSuccess, onError));
    }

    private IEnumerator LoadIcon(string url, Action<Sprite> onSuccess, Action<string> onError)
    {
        using var request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning($"Failed to load icon from {url}: {request.error}");
            onError?.Invoke(request.error);
            yield break;
        }

        var texture = DownloadHandlerTexture.GetContent(request);
        var sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );

        _cache[url] = sprite;
        onSuccess?.Invoke(sprite);
    }
}