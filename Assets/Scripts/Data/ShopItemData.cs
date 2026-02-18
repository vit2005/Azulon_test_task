using UnityEngine;

public abstract class ShopItemData : ScriptableObject
{
    public int id;
    public string name;
    public abstract ItemType type { get; }
    [TextArea] public string description;
    public Sprite icon;
    public CurrencyType currencyType;
    public int price;
}
