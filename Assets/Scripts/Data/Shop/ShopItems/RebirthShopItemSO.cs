using UnityEngine;

[CreateAssetMenu(fileName = "RebirthShopItem", menuName = "Shop/Rebirth")]
public class RebirthShopItemSO : ShopItemDataSO
{
    public RebirthShopItem data;
    public override ShopItemData GetData() => data;
}