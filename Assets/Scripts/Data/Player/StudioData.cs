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

    /* TODO: Move this to specific classes if there will be more states with different mechanics 
    public void StartCrunch(float intencity)
    {
        programmersState = ProgrammersState.Crunching;
        stateStartTime = Time.time;
        crunchIntencity = intencity;
    }

    public void Rebirth()
    {
        programmers.Clear();
        programmersMaxAmount = 0;
        programmersState = ProgrammersState.Idle;
        stateStartTime = 0f;
        crunchIntencity = 1f;
    }
    */
}
