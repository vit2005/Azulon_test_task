using UnityEngine;

[CreateAssetMenu(fileName = "WorkspaceShopItem", menuName = "Shop/Workspace")]
public class WorkspaceShopItemSO : ShopItemDataSO
{
    public WorkspaceShopItem data;
    public override ShopItemData GetData() => data;
}