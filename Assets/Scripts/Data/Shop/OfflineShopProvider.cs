using System;
using System.Collections.Generic;
using System.Linq;

public class OfflineShopProvider : IShopProvider
{
    private readonly List<ShopItemDataSO> _items;

    public OfflineShopProvider(List<ShopItemDataSO> items)
    {
        _items = items;
    }

    public void GetItems(Action<List<ShopItemData>> onSuccess, Action<string> onError)
    {
        var data = _items.Select(so =>
        {
            var item = so.GetData();
            item.icon = so.icon;
            return item;
        }).ToList();

        onSuccess?.Invoke(data);
    }
}