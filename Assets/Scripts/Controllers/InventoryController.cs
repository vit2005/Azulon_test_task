using UnityEngine;

public class InventoryController
{
    private PlayerData _playerData;

    public InventoryController(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void SetProgrammer(ProgrammerItem programmerData)
    {
        if (_playerData.studioData.programmers.Count == _playerData.studioData.programmersMaxAmount) return;

        _playerData.inventoryData.programmers.Remove(programmerData);
        _playerData.inventoryData.NotifyUpdated();
        _playerData.studioData.programmers.Add(programmerData);
        _playerData.studioData.NotifyUpdated();
    }

    public void FreeProgrammer(ProgrammerItem programmerData)
    {
        _playerData.studioData.programmers.Remove(programmerData);
        _playerData.studioData.NotifyUpdated();
        _playerData.inventoryData.programmers.Add(programmerData);
        _playerData.inventoryData.NotifyUpdated();
    }

    public void SellProgrammer(ProgrammerItem programmerData)
    {
        _playerData.inventoryData.programmers.Remove(programmerData);
        _playerData.inventoryData.NotifyUpdated();
        _playerData.currencies[CurrencyType.Gold] += programmerData.price / 2; // Sell for half price
        _playerData.NotifyUpdated();
    }

}
