using UnityEngine;

[CreateAssetMenu(fileName = "ProgrammerShopItem", menuName = "Scriptable Objects/ProgrammerShopItem")]
public class ProgrammerShopItem : ShopItemData
{
    public override ItemType type => ItemType.Programmer;

    public float income;
}
