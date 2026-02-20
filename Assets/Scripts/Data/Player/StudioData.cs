using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StudioData
{
    public List<ProgrammerItem> programmers = new List<ProgrammerItem>();
    public int programmersMaxAmount = 0; // workspace size
    public ProgrammersState programmersState = ProgrammersState.Idle;
    public float stateStartTime = 0f; // time when the current state started (crunch or burnout)
    public float crunchIntensity = 1f; // how much more productive programmers are during crunch

    public event Action OnDataUpdated;
    public void NotifyUpdated() => OnDataUpdated?.Invoke();

}
