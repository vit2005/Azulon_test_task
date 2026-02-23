using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI price;

    public void Init(ShopItemData data, Action<ShopItemData> onBuy)
    {
        name.text = data.name;
        icon.sprite = data.icon;
        price.text = data.price.ToString();
        buyButton.onClick.AddListener(() => onBuy?.Invoke(data));
    }
}
