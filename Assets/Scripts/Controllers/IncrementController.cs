using System;
using System.Collections;
using UnityEngine;

public class IncrementController
{
    private float _goldPerSecond = 0;
    private PlayerData _playerData;

    public IncrementController(PlayerData playerData, MonoBehaviour runner)
    {
        _goldPerSecond = CalculateGoldPerSecond(playerData);
        _playerData = playerData;
        playerData.OnDataUpdated += () => _goldPerSecond = CalculateGoldPerSecond(playerData);
        runner.StartCoroutine(IncrementGold());
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

    private float CalculateGoldPerSecond(PlayerData playerData)
    {
        float baseIncome = 1f;
        float programmers = 0f;
        foreach (var item in playerData.studioData.programmers)
        {
            programmers += item.income;
        }
        float crunchMultiplier = playerData.studioData.crunchIntensity;

        return baseIncome + programmers * crunchMultiplier;
    }
}
