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
        InitPlayerData();
        InitInventory();
        InitShopItems();
    }


    private void InitPlayerData()
    {
        gameplayUI.Init(GameController.Instance.PlayerData);
    }
    private void InitInventory()
    {
        inventoryUI.Init(GameController.Instance.PlayerData);
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

    public void HideShop()
    {
        // TODO: implement animation
        shopUI.gameObject.SetActive(false);
    }

    public void ShowInventory()
    {
        // TODO: implement animation
        inventoryUI.gameObject.SetActive(true);
    }

    public void HideInventory()
    {
        // TODO: implement animation
        inventoryUI.gameObject.SetActive(false);
    }
}