using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController
{
    private static GameController _instance;
    public static GameController Instance => _instance;

    private Action _onInitializaed;

    public ShopService ShopService { get; }
    public PlayerData PlayerData { get; private set; }
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
        CrunchController = new CrunchController(PlayerData.studioData);
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

        //shopProvider.TryPurchaseItem(data, OnItemPurchased, OnItemPurchaseFailed);
    }

    //private void OnItemPurchased()
    //{
    //    throw new NotImplementedException();
    //}

    //private void OnItemPurchaseFailed(string error)
    //{
    //    throw new NotImplementedException();
    //}

    public bool OnDestroy()
    {
        playerStorageProvider.Save(PlayerData, () => Debug.Log("Player data saved successfully"), (string e) => Debug.LogError($"Failed to save player data: {e}"));
        return true;
    }

    public void SetProgrammer(ProgrammerItem programmerData)
    {
        if (PlayerData.studioData.programmers.Count == PlayerData.studioData.programmersMaxAmount) return;

        PlayerData.inventoryData.programmers.Remove(programmerData);
        PlayerData.inventoryData.NotifyUpdated();
        PlayerData.studioData.programmers.Add(programmerData);
        PlayerData.studioData.NotifyUpdated();
    }

    public void FreeProgrammer(ProgrammerItem programmerData)
    {
        PlayerData.studioData.programmers.Remove(programmerData);
        PlayerData.studioData.NotifyUpdated();
        PlayerData.inventoryData.programmers.Add(programmerData);
        PlayerData.inventoryData.NotifyUpdated();
    }

    public void SellProgrammer(ProgrammerItem programmerData)
    {
        PlayerData.inventoryData.programmers.Remove(programmerData);
        PlayerData.inventoryData.NotifyUpdated();
        PlayerData.currencies[CurrencyType.Gold] += programmerData.price / 2; // Sell for half price
    }
}