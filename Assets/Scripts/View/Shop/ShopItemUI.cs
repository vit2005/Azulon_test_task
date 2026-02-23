using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image currencyIcon;
    [SerializeField] Animation animation;

    private ShopItemData _data;
    Action<ShopItemData> _onBuy;

    public void Init(ShopItemData data, Sprite currencyIcon, Action<ShopItemData> onBuy)
    {
        _data = data;
        _onBuy = onBuy;
        name.text = data.name;
        icon.sprite = data.icon;
        price.text = data.price.ToString();
        this.currencyIcon.sprite = currencyIcon;
        description.text = data.description;
    }

    public void OnBuyClick()
    {
        animation.Play();
        _onBuy?.Invoke(_data);
    }
}
