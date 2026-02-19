using System;
using System.Collections.Generic;
using UnityEngine;

public interface IShopProvider
{
    void GetItems(Action<List<ShopItemData>> onSuccess, Action<string> onError);
}