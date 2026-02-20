using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    public List<ProgrammerItem> programmers = new List<ProgrammerItem>();

    public event Action OnDataUpdated;
    public void NotifyUpdated() => OnDataUpdated?.Invoke();

}
