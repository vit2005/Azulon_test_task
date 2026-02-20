using System;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] ShopUI shopUI;

    private void Awake()
    {
        GameController.Instance.InitMainUI(this);

        InitShopItems();
    }

    private void InitShopItems()
    {
        GameController.Instance.shopProvider.GetItems(
            OnShopItemsLoadedSuccessfully, OnShopItemsLoadFailed);
    }

    private void OnShopItemsLoadFailed(string obj)
    {
        // TODO: Make popup with error message
    }

    private void OnShopItemsLoadedSuccessfully(List<ShopItemData> list)
    {
        shopUI.Init(list);
    }
}
