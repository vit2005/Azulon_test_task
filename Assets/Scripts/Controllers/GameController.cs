using System;
using System.Linq;
using UnityEngine;

public class GameController
{
    private static GameController _instance;
    public static GameController Instance => _instance;

    public ShopService ShopService { get; }
    public PlayerData PlayerData { get; private set; }
    public CrunchController CrunchController { get; private set; }
    public RebirthController RebirthController { get; private set; }

    public MainUI mainUI { get; private set; }

    public MonoBehaviour runner { get; private set; }

    public IShopProvider shopProvider { get; private set; }
    public IPlayerStorage playerStorageProvider { get; private set; }

    // TODO: init OfflineShopProvider and OfflinePlayerStorage here
    public GameController()
    {
        _instance = this;

        InitOfflineShopProvider();
        InitOfflinePlayerStorage();

        playerStorageProvider.Load(OnPlayerDataLoaded, (string e) => Debug.LogError(e));

        ShopService = new ShopService(this);
    }

    public void OnPlayerDataLoaded(PlayerData playerData)
    {
        PlayerData = playerData;
        CrunchController = new CrunchController(PlayerData.studioData);
        RebirthController = new RebirthController(PlayerData);
    }

    public void InitRunner(MonoBehaviour runner)
    {
        this.runner = runner;
    }

    public void InitMainUI(MainUI mainUI)
    {
        this.mainUI = mainUI;
    }

    public void InitOfflineShopProvider()
    {
        var shopItems = Resources.LoadAll<ShopItemDataSO>("ShopItems");
        shopProvider = new OfflineShopProvider(shopItems.OrderBy(x => x.GetData().price).ToList());
    }

    public void InitOfflinePlayerStorage()
    {
        playerStorageProvider = new OfflinePlayerStorage();
    }

    public void BuyItem(ShopItemData data)
    {
        ShopService.Purchase(data, PlayerData);

        //shopProvider.TryPurchaseItem(data, OnItemPurchased, OnItemPurchaseFailed);
    }

    private void OnItemPurchased()
    {
        throw new NotImplementedException();
    }

    private void OnItemPurchaseFailed(string error)
    {
        throw new NotImplementedException();
    }
}