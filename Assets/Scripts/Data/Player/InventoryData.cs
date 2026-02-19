using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    public List<ProgrammerItem> programmers = new List<ProgrammerItem>();

    public event Action OnDataUpdated;
    public void NotifyUpdated() => OnDataUpdated?.Invoke();

    /* TODO: Move this to specific classes if there will be more states with different mechanics
    public void Rebirth()
    {
        programmers.Clear();
    }
    */
}
