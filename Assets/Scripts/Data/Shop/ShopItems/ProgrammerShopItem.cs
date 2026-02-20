using System;
using UnityEngine;

[Serializable]
public class ProgrammerShopItem : ShopItemData
{
    public override ItemType type => ItemType.Programmer;
    public float income;
}