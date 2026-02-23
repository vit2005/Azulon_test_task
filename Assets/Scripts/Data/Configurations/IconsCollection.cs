using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IconData
{
    public int id;
    public Sprite icon;
}

[CreateAssetMenu(fileName = "IconsCollection", menuName = "Scriptable Objects/IconsCollection")]
public class IconsCollection : ScriptableObject
{
    public List<IconData> icons;
}
