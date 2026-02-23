using UnityEngine;

public enum ScreenType
{
    Gameplay,
    Inventory,
    Shop
}

public interface IScreen
{
    public void Show();
    public void Hide();
}
