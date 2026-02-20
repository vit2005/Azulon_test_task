using System;
using UnityEngine;

[Serializable]
public class CrunchShopItem : ShopItemData
{
    public override ItemType type => ItemType.Crunch;
    public float intensity;
}