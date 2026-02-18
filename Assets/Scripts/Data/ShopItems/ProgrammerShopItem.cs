using UnityEngine;

[CreateAssetMenu(fileName = "ProgrammerShopItem", menuName = "Scriptable Objects/ProgrammerShopItem")]
public class ProgrammerShopItem : ShopItemData
{
    public override ItemType type => ItemType.Programmer;

    public float income;

    public override bool CanPurchase(PlayerData playerData)
    {
        return base.CanPurchase(playerData) && 
            playerData.studioData.programmersMaxAmount > playerData.studioData.programmers.Count + 1;
    }

    public override void ApplyPurchase(PlayerData playerData)
    {
        playerData.inventoryData.programmers.Add(new ProgrammerItem() { income = income });
    }
}
