using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CurrencyData
{
    public CurrencyType type;
    public Sprite icon;
}

[CreateAssetMenu(fileName = "CurrenciesCollection", menuName = "Scriptable Objects/CurrenciesCollection")]
public class CurrenciesCollection : ScriptableObject
{
    public List<CurrencyData> list = new List<CurrencyData>();
}
