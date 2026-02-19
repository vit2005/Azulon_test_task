using System.Collections.Generic;
using UnityEngine;

public class StudioData
{
    public List<ProgrammerItem> programmers = new List<ProgrammerItem>();
    public int programmersMaxAmount = 0; // workspace size
    public ProgrammersState programmersState = ProgrammersState.Idle;
    public float stateStartTime = 0f; // time when the current state started (crunch or burnout)
    private float crunchIntencity = 1f; // how much more productive programmers are during crunch

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
}
