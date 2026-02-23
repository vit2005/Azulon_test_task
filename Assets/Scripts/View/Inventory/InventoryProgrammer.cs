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

    public void Init(ProgrammerItem data)
    {
        _data = data;
        icon.sprite = data.icon;
        description.text = $"Title: {data.name}\nIncome: {data.income}";
    }

    public void OnPlaceClick()
    {
        GameController.Instance.SetProgrammer(_data);
    }

    public void OnSellClick()
    {
        GameController.Instance.SellProgrammer(_data);
    }
}
