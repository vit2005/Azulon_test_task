using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ShopItemsWrapper
{
    public List<ShopItemData> items;
}

public class OnlineShopProvider : IShopProvider
{
    private readonly string _url;
    private readonly MonoBehaviour _runner;
    private readonly IconLoader _iconLoader;

    public OnlineShopProvider(string url, MonoBehaviour runner, IconLoader iconLoader)
    {
        _url = url;
        _runner = runner;
        _iconLoader = iconLoader;
    }

    public void GetItems(Action<List<ShopItemData>> onSuccess, Action<string> onError)
    {
        _runner.StartCoroutine(FetchItems(onSuccess, onError));
    }

    private IEnumerator FetchItems(Action<List<ShopItemData>> onSuccess, Action<string> onError)
    {
        using var request = UnityWebRequest.Get(_url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);
            yield break;
        }

        var wrapper = JsonUtility.FromJson<ShopItemsWrapper>(request.downloadHandler.text);
        var items = wrapper.items;

        yield return LoadIcons(items);

        onSuccess?.Invoke(items);
    }

    private IEnumerator LoadIcons(List<ShopItemData> items)
    {
        var remaining = 0;

        foreach (var item in items)
        {
            if (string.IsNullOrEmpty(item.iconURL)) continue;

            remaining++;
            _iconLoader.Load(
                item.iconURL,
                sprite =>
                {
                    item.icon = sprite;
                    remaining--;
                },
                _ => remaining--
            );
        }

        yield return new WaitUntil(() => remaining == 0);
    }
}