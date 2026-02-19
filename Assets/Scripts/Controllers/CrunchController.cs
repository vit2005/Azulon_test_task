using UnityEngine;

public class CrunchController
{
    private readonly StudioData _studioData;

    public CrunchController(StudioData studioData)
    {
        _studioData = studioData;
    }

    public void StartCrunch(float intensity)
    {
        _studioData.programmersState = ProgrammersState.Crunching;
        _studioData.stateStartTime = Time.time;
        _studioData.crunchIntensity = intensity;
        _studioData.NotifyUpdated();
    }
}