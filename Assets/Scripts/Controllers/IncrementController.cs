using System;
using System.Collections;
using UnityEngine;

public class IncrementController
{
    private float _goldPerSecond = 0;
    private PlayerData _playerData;
    private MonoBehaviour _runner;
    private Coroutine _incrementCoroutine;

    public IncrementController(PlayerData playerData, MonoBehaviour runner)
    {
        _runner = runner;
        CalculateGoldPerSecond(playerData);
        _playerData = playerData;
        playerData.OnDataUpdated += () => CalculateGoldPerSecond(playerData);
        _incrementCoroutine = runner.StartCoroutine(IncrementGold());
    }

    private IEnumerator IncrementGold()
    {
        while (true)
        {
            _playerData.currencies[CurrencyType.Gold] += _goldPerSecond;
            _playerData.NotifyUpdated();
            yield return new WaitForSeconds(1f);
        }
    }

    private void CalculateGoldPerSecond(PlayerData playerData)
    {
        float baseIncome = 1f;
        float rebirth = (float)Math.Sqrt(playerData.rebirths);
        float programmers = 0f;
        foreach (var item in playerData.studioData.programmers)
        {
            programmers += item.income;
        }
        float crunchMultiplier = playerData.studioData.crunchIntensity;

        _goldPerSecond = baseIncome + programmers * crunchMultiplier + rebirth;
    }
}
