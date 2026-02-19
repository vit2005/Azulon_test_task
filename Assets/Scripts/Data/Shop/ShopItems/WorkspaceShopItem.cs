using System;
using UnityEngine;

[Serializable]
public class WorkspaceShopItem : ShopItemData
{
    public override ItemType type => ItemType.Workspace;
    public int WorkPlacesAmount;
}

[CreateAssetMenu(fileName = "WorkspaceShopItem", menuName = "Shop/Workspace")]
public class WorkspaceShopItemSO : ShopItemDataSO
{
    public WorkspaceShopItem data;
    public override ShopItemData GetData() => data;
}