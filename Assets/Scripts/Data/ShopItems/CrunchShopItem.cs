using UnityEngine;

[CreateAssetMenu(fileName = "CrunchShopItem", menuName = "Scriptable Objects/CrunchShopItem")]
public class CrunchShopItem : ShopItemData
{
    public override ItemType type => ItemType.Crunch;
    public float intencity;
}
