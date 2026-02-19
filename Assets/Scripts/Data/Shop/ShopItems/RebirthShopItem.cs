using System;
using UnityEngine;

[Serializable]
public class RebirthShopItem : ShopItemData
{
    public override ItemType type => ItemType.Rebrth;
}

[CreateAssetMenu(fileName = "RebirthShopItem", menuName = "Shop/Rebirth")]
public class RebirthShopItemSO : ShopItemDataSO
{
    public RebirthShopItem data;
    public override ShopItemData GetData() => data;
}