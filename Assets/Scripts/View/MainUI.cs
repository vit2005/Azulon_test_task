using System;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GameplayUI gameplayUI;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] ShopUI shopUI;

    private void Awake()
    {
        GameController.Instance.InitMainUI(this);

        InitPlayerData();
        InitShopItems();
    }

    private void InitPlayerData()
    {
        GameController.Instance.PlayerData.OnDataUpdated += 
            () => gameplayUI.UpdateData(GameController.Instance.PlayerData);
        gameplayUI.UpdateData(GameController.Instance.PlayerData);
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

    public void ShowShop()
    {
        // TODO: implement animation
        shopUI.gameObject.SetActive(true);
    }

    public void ShowInventory()
    {
        // TODO: implement animation
        inventoryUI.gameObject.SetActive(true);
    }
}
