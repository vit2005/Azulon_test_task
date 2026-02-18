using UnityEngine;

[CreateAssetMenu(fileName = "WorkspaceShopItem", menuName = "Scriptable Objects/WorkspaceShopItem")]
public class WorkspaceShopItem : ShopItemData
{
    public override ItemType type => ItemType.Workspace;
    public int WorkPlacesAmount;
}
