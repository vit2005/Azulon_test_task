using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public Dictionary<CurrencyType, int> currencies = new Dictionary<CurrencyType, int>();
    public int rebirths = 0; // multiplier of income
    public StudioData studioData = new StudioData();
    public InventoryData inventoryData = new InventoryData();

    public void PutProgrammerToWorkplace(ProgrammerItem programmer)
    {
        if (studioData.programmersMaxAmount > studioData.programmers.Count)
        {
            studioData.programmers.Add(programmer);
            inventoryData.programmers.Remove(programmer);
        }
    }
}
