using UnityEngine;

[CreateAssetMenu(fileName = "ProgrammerShopItem", menuName = "Shop/Programmer")]
public class ProgrammerShopItemSO : ShopItemDataSO
{
    public ProgrammerShopItem data;
    public override ShopItemData GetData() => data;
}