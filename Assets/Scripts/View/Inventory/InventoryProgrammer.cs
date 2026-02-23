using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryProgrammerUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI description;

    private ProgrammerItem _data;
    public ProgrammerItem Data => _data;

    private Action<ProgrammerItem> _onSetClick;
    private Action<ProgrammerItem> _onSellClick;

    public void Init(ProgrammerItem data, 
        Action<ProgrammerItem> onSetClick, Action<ProgrammerItem> onSellClick)
    {
        _data = data;
        _onSetClick = onSetClick;
        _onSellClick = onSellClick;
        icon.sprite = data.icon;
        description.text = $"Title: {data.name}\nIncome: {data.income}";
    }

    public void OnPlaceClick()
    {
        _onSetClick?.Invoke(_data);
    }

    public void OnSellClick()
    {
        _onSellClick?.Invoke(_data);
    }
}
