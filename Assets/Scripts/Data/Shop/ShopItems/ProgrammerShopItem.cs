using System;
using UnityEngine;

[Serializable]
public class ProgrammerShopItem : ShopItemData
{
    public override ItemType type => ItemType.Programmer;
    public float income;
}

[CreateAssetMenu(fileName = "ProgrammerShopItem", menuName = "Shop/Programmer")]
public class ProgrammerShopItemSO : ShopItemDataSO
{
    public ProgrammerShopItem data;
    public override ShopItemData GetData() => data;
}