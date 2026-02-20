using System;
using UnityEngine;

[Serializable]
public class WorkspaceShopItem : ShopItemData
{
    public override ItemType type => ItemType.Workspace;
    public int WorkPlacesAmount;
}