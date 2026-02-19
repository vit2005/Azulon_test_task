using System;
using UnityEngine;

/// <summary>
/// Uses for JSON deserialization of shop items. 
/// Each item type should have its own class inherited from this one
/// </summary>
[Serializable]
public abstract class ShopItemData
{
    public int id;
    public string name;
    public abstract ItemType type { get; }
    [TextArea] public string description;
    public CurrencyType currencyType;
    public int price;
    public string iconURL;

    [NonSerialized] public Sprite icon;
}

/// <summary>
/// Uses for Offline shop items. Each item type should have its own class inherited from this one
/// </summary>
public abstract class ShopItemDataSO : ScriptableObject
{
    public Sprite icon;
    public abstract ShopItemData GetData();
}