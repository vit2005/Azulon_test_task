using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopService
{
    private readonly Dictionary<ItemType, Action<ShopItemData, PlayerData>> _handlers = new();

    private void Register<T>(ItemType type, Action<T, PlayerData> handler) where T : ShopItemData
    {
        _handlers[type] = (item, data) => handler((T)item, data);
    }

    public ShopService()
    {
        Register<CrunchShopItem>(ItemType.Crunch, (item, data) =>
            data.studioData.StartCrunch(item.intencity));

        Register<WorkspaceShopItem>(ItemType.Workspace, (item, data) =>
            data.studioData.programmersMaxAmount += item.WorkPlacesAmount);

        Register<ProgrammerShopItem>(ItemType.Programmer, (item, data) =>
            data.inventoryData.programmers.Add(new ProgrammerItem { income = item.income }));

        Register<RebrthShopItem>(ItemType.Rebrth, (item, data) =>
        {
            data.inventoryData.Rebirth();
            data.studioData.Rebirth();
        });
    }

    public void Purchase(ShopItemData item, PlayerData playerData)
    {
        if (!CanPurchase(item, playerData)) return;

        playerData.currencies[item.currencyType] -= item.price;
        _handlers[item.type](item, playerData);
    }

    public bool CanPurchase(ShopItemData item, PlayerData playerData) =>
        playerData.currencies.TryGetValue(item.currencyType, out var balance)
        && balance >= item.price;
}
