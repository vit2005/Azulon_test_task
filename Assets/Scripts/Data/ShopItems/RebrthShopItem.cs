using UnityEngine;

[CreateAssetMenu(fileName = "RebrthShopItem", menuName = "Scriptable Objects/RebrthShopItem")]
public class RebrthShopItem : ShopItemData
{
    public override ItemType type => ItemType.Rebrth;

    public override void ApplyPurchase(PlayerData playerData)
    {
        playerData.inventoryData.Rebirth();
        playerData.studioData.Rebirth();
    }
}
