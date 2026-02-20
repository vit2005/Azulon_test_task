using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemTypeItemPanel
{
    public ItemType itemType;
    public GameObject itemPanel;
}

public class ShopUI : MonoBehaviour
{
    [SerializeField] List<ItemTypeItemPanel> panels;
    [SerializeField] GameObject itemPanelPrefab;

    private List<GameObject> _instances = new List<GameObject>();

    public void Init(List<ShopItemData> items)
    {
        foreach (var item in items)
        {
            var panel = panels.Find(p => p.itemType == item.type)?.itemPanel; // TODO: make dictionary for better performance
            if (panel != null)
            {
                var instance = Instantiate(itemPanelPrefab, panel.transform);
                instance.GetComponent<ShopItemUI>().Init(item, OnBuy);
                _instances.Add(instance);
            }
        }
    }

    public void OnBuy(ShopItemData data)
    {
        GameController.Instance.BuyItem(data); // TODO: add popups for purchase result
    }

    
}
