using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int _startingMoneyAmount = 1000;

    public int CurrentMoneyAmount { get; private set; }
    public Action OnChangeMoneyAmount;

    private void Awake() => CurrentMoneyAmount = _startingMoneyAmount;

    private void Update()
    {
        Debug.Log(CurrentMoneyAmount);
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
