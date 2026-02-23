using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemTypeItemPanel
{
    public ItemType itemType;
    public GameObject itemPanel;
}

public class ShopUI : AnimatedScreen
{
    [SerializeField] List<ItemTypeItemPanel> panels;
    [SerializeField] GameObject itemPanelPrefab;
    [SerializeField] CurrenciesCollection currenciesIconsCollection;

    private List<GameObject> _instances = new List<GameObject>();
    private Dictionary<CurrencyType, Sprite> _currencyIcons;


    public void Init(List<ShopItemData> items)
    {
        InitCurrenciesIconsDictionary();
        foreach (var item in items)
        {
            var panel = panels.Find(p => p.itemType == item.type)?.itemPanel; // TODO: make dictionary for better performance
            if (panel != null)
            {
                var instance = Instantiate(itemPanelPrefab, panel.transform);
                instance.GetComponent<ShopItemUI>().Init(item, _currencyIcons[item.currencyType], OnBuy);
                _instances.Add(instance);
            }
        }
    }

    private void InitCurrenciesIconsDictionary()
    {
        _currencyIcons = new Dictionary<CurrencyType, Sprite>();
        foreach (var currency in currenciesIconsCollection.list)
        {
            _currencyIcons.Add(currency.type, currency.icon);
        }
    }

    public void OnBuy(ShopItemData data)
    {
        GameController.Instance.BuyItem(data); // TODO: add popups for purchase result
    }
}
