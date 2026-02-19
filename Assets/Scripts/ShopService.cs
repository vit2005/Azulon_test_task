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

    // TODO: if item types count will rise - implement separate handlers for each type and move this logic to them
    public ShopService()
    {
        Register<CrunchShopItem>(ItemType.Crunch, (item, data) =>
            GameController.Instance.CrunchController.StartCrunch(item.intencity));

        Register<WorkspaceShopItem>(ItemType.Workspace, (item, data) =>
            data.studioData.programmersMaxAmount += item.WorkPlacesAmount);

        Register<ProgrammerShopItem>(ItemType.Programmer, (item, data) =>
            data.inventoryData.programmers.Add(new ProgrammerItem { income = item.income }));

        Register<RebirthShopItem>(ItemType.Rebrth, (item, data) =>
        {
            GameController.Instance.RebirthController.Rebirth();
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
