using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public List<ProgrammerItem> programmers = new List<ProgrammerItem>();

    public void Rebirth()
    {
        programmers.Clear();
    }
}
