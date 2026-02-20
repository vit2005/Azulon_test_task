using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Button buyButton;

    public void Init(ShopItemData data, Action<ShopItemData> onBuy)
    {
        name.text = data.name;
        icon.sprite = data.icon;
        buyButton.onClick.AddListener(() => onBuy?.Invoke(data));
    }
}
