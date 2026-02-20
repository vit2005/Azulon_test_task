using UnityEngine;

[CreateAssetMenu(fileName = "CrunchShopItem", menuName = "Shop/Crunch")]
public class CrunchShopItemSO : ShopItemDataSO
{
    public CrunchShopItem data;
    public override ShopItemData GetData() => data;
}
