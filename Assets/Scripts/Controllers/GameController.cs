using System;
using System.Linq;
using UnityEngine;

public class GameController
{
    private static GameController _instance;
    public static GameController Instance => _instance;

    private Action _onInitializaed;

    public ShopService ShopService { get; }
    public PlayerData PlayerData { get; private set; }
    public InventoryController InventoryController { get; private set; }
    public CrunchController CrunchController { get; private set; }
    public RebirthController RebirthController { get; private set; }
    public IncrementController IncrementController { get; private set; }

    public MonoBehaviour runner { get; private set; }

    public IShopProvider shopProvider { get; private set; }
    public IPlayerStorage playerStorageProvider { get; private set; }

    // TODO: init OfflineShopProvider and OfflinePlayerStorage here
    public GameController(Action onInitializaed, MonoBehaviour runner)
    {
        _instance = this;
        this.runner = runner;
        _onInitializaed = onInitializaed;

        InitOfflineShopProvider();
        InitOfflinePlayerStorage();

        playerStorageProvider.Load(OnPlayerDataLoaded, (string e) => Debug.LogError(e));

        ShopService = new ShopService(this);
    }

    public void OnPlayerDataLoaded(PlayerData playerData)
    {
        PlayerData = playerData;
        InventoryController = new InventoryController(PlayerData);
        CrunchController = new CrunchController(PlayerData.studioData, runner);
        RebirthController = new RebirthController(PlayerData);
        IncrementController = new IncrementController(PlayerData, runner);
        Application.wantsToQuit += OnDestroy;
        _onInitializaed?.Invoke();
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
    }

    public bool OnDestroy()
    {
        playerStorageProvider.Save(PlayerData, () => Debug.Log("Player data saved successfully"), (string e) => Debug.LogError($"Failed to save player data: {e}"));
        return true;
    }
}