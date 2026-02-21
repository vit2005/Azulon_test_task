using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public Dictionary<CurrencyType, float> currencies = new Dictionary<CurrencyType, float>();
    public int rebirths = 0; // multiplier of income
    public StudioData studioData = new StudioData();
    public InventoryData inventoryData = new InventoryData();

    public event Action OnDataUpdated;
    public void NotifyUpdated() => OnDataUpdated?.Invoke();

    /* TODO: move to GameplayController
    public void PutProgrammerToWorkplace(ProgrammerItem programmer)
    {
        if (studioData.programmersMaxAmount > studioData.programmers.Count)
        {
            studioData.programmers.Add(programmer);
            inventoryData.programmers.Remove(programmer);
        }
    }
    */
}
