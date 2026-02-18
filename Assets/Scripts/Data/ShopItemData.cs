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

    public virtual bool CanPurchase(PlayerData playerData)
    {
        return playerData.currencies.ContainsKey(currencyType) && playerData.currencies[currencyType] >= price;
    }

    public virtual void Purchase(PlayerData playerData)
    {
        if (CanPurchase(playerData))
        {
            playerData.currencies[currencyType] -= price;
            ApplyPurchase(playerData);
        }
    }

    public abstract void ApplyPurchase(PlayerData playerData);
}
