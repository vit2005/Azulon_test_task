using System;
using UnityEngine;

[Serializable]
public class CrunchShopItem : ShopItemData
{
    public override ItemType type => ItemType.Crunch;
    public float intencity;
}

[CreateAssetMenu(fileName = "CrunchShopItem", menuName = "Shop/Crunch")]
public class CrunchShopItemSO : ShopItemDataSO
{
    public CrunchShopItem data;
    public override ShopItemData GetData() => data;
}