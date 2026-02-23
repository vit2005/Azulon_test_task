using System;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GameplayUI gameplayUI;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] ShopUI shopUI;

    public ScreenType currentScreenType = ScreenType.Gameplay;
    public IScreen currentScreen;

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

    public void ChangeState(ScreenType type, IScreen screen)
    {
        currentScreen?.Hide();
        currentScreen = screen;
        currentScreenType = type;
        currentScreen?.Show();
    }

    public void ShowShop()
    {
        ChangeState(ScreenType.Shop, shopUI);
    }

    public void HideShop()
    {
        ChangeState(ScreenType.Gameplay, null);
    }

    public void ShowInventory()
    {
        ChangeState(ScreenType.Inventory, inventoryUI);
    }

    public void HideInventory()
    {
        ChangeState(ScreenType.Gameplay, null);
    }
}