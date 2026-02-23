using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour, IScreen
{
    [SerializeField] GameObject inventoryProgrammerPrefab;
    [SerializeField] Transform parent;
    [SerializeField] IconsCollection iconsCollection;
    private PlayerData _playerData;
    private List<InventoryProgrammerUI> _instances = new List<InventoryProgrammerUI>();


    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
        LoadIcons();
        _playerData.inventoryData.OnDataUpdated += UpdateUI;
        UpdateUI();
    }

    public void Show()
    {
        // TODO: implement animation
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        // TODO: implement animation
        gameObject.SetActive(false);
    }

    private void LoadIcons()
    {
        foreach (var item in _playerData.inventoryData.programmers)
        {
            item.icon = iconsCollection.icons.First(x => x.id == item.iconId).icon;
        }
    }

    private void UpdateUI()
    {
        foreach (var item in _playerData.inventoryData.programmers)
        {
            if (_instances.Any(x => x.Data == item)) continue;

            var instance = Instantiate(inventoryProgrammerPrefab, parent).GetComponent<InventoryProgrammerUI>();
            instance.Init(item, OnSetClick, OnSellClick);
            _instances.Add(instance);
        }

        List<InventoryProgrammerUI> instancesToRemove = new List<InventoryProgrammerUI>();
        foreach (var item in _instances)
        {
            if (_playerData.inventoryData.programmers.Any(x => x == item.Data)) continue;
            instancesToRemove.Add(item);
        }

        for (int i = 0; i < instancesToRemove.Count; i++)
        {
            Destroy(instancesToRemove[i].gameObject);
            _instances.Remove(instancesToRemove[i]);
        }
    }

    private void OnSetClick(ProgrammerItem data)
    {
        GameController.Instance.SetProgrammer(data);
    }

    private void OnSellClick(ProgrammerItem data)
    {
        GameController.Instance.SellProgrammer(data);
    }

    private void OnDestroy()
    {
        _playerData.inventoryData.OnDataUpdated -= UpdateUI;
    }
}
