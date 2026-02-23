using System;
using System.Collections;
using UnityEngine;

public class CrunchController
{
    public const float CrunchDuration = 10f;
    public const float BurnoutDuration = 5f;
    public const float BurnoutMultiplier = 0.5f;

    private readonly StudioData _studioData;
    private readonly MonoBehaviour _runner;
    private Coroutine _coroutine;

    public event Action OnStateChanged;

    public CrunchController(StudioData studioData, MonoBehaviour runner)
    {
        _studioData = studioData;
        _runner = runner;

        RestoreState();
    }

    /// <summary>
    /// ¬икликаЇтьс€ п≥сл€ завантаженн€ даних.
    /// secondsLeft вже збережено Ч просто п≥дхоплюЇмо корутину з залишком.
    /// </summary>
    public void RestoreState()
    {
        switch (_studioData.programmersState)
        {
            case ProgrammersState.Crunching:
                _coroutine = _runner.StartCoroutine(CrunchRoutine());
                break;
            case ProgrammersState.Burnout:
                _coroutine = _runner.StartCoroutine(BurnoutRoutine());
                break;
        }
    }

    public void StartCrunch(float intensity)
    {
        // if (_studioData.programmersState == ProgrammersState.Burnout) return;

        StopCurrentCoroutine();

        _studioData.programmersState = ProgrammersState.Crunching;
        _studioData.secondsLeft = CrunchDuration;
        _studioData.crunchIntensity = intensity;
        _studioData.NotifyUpdated();
        OnStateChanged?.Invoke();

        _coroutine = _runner.StartCoroutine(CrunchRoutine());
    }

    private IEnumerator CrunchRoutine()
    {
        while (_studioData.secondsLeft > 0f)
        {
            yield return null; // чекаЇмо один кадр
            _studioData.secondsLeft -= Time.deltaTime;
        }

        _studioData.secondsLeft = 0f;
        EnterBurnout();
    }

    private void EnterBurnout()
    {
        _studioData.programmersState = ProgrammersState.Burnout;
        _studioData.secondsLeft = BurnoutDuration;
        _studioData.crunchIntensity = BurnoutMultiplier;
        _studioData.NotifyUpdated();
        OnStateChanged?.Invoke();

        _coroutine = _runner.StartCoroutine(BurnoutRoutine());
    }

    private IEnumerator BurnoutRoutine()
    {
        while (_studioData.secondsLeft > 0f)
        {
            yield return null;
            _studioData.secondsLeft -= Time.deltaTime;
        }

        _studioData.secondsLeft = 0f;
        EndBurnout();
    }

    private void EndBurnout()
    {
        _studioData.programmersState = ProgrammersState.Idle;
        _studioData.secondsLeft = 0f;
        _studioData.crunchIntensity = 1f;
        _studioData.NotifyUpdated();
        OnStateChanged?.Invoke();
    }

    private void StopCurrentCoroutine()
    {
        if (_coroutine != null)
        {
            _runner.StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}