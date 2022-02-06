using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int _startingMoneyAmount = 1000;
    [SerializeField] private int _awardForFinishingWave = 100;

    public int CurrentMoneyAmount { get; private set; }
    public Action OnChangeMoneyAmount;

    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        CurrentMoneyAmount = _startingMoneyAmount;

        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += AddMoneyForFinishingWave;
    }

    private void OnDestroy() => _waveSpawner.OnFinishWave -= AddMoneyForFinishingWave;

    private void AddMoneyForFinishingWave()
    {
        CurrentMoneyAmount += _awardForFinishingWave;
        OnChangeMoneyAmount?.Invoke();
    }

    public void IncreaseMoneyAmount(int amount)
    {
        CurrentMoneyAmount += amount;
        OnChangeMoneyAmount?.Invoke();
    }

    public void DecreaseMoneyAmount(int amount)
    {
        CurrentMoneyAmount -= amount;
        OnChangeMoneyAmount?.Invoke();
    }
}
