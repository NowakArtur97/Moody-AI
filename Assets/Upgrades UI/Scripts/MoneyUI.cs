using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    private const string DOLLAR_SIGN = "$";

    [SerializeField] private TMP_Text _currentMoneyAmountText;

    private MoneyManager _moneyManager;

    private void Awake()
    {
        _moneyManager = FindObjectOfType<MoneyManager>();
        _moneyManager.OnChangeMoneyAmount += ChangeDisplayedMoneyAmount;
    }

    private void Start() => ChangeDisplayedMoneyAmount();

    private void OnDestroy() => _moneyManager.OnChangeMoneyAmount -= ChangeDisplayedMoneyAmount;

    private void ChangeDisplayedMoneyAmount() => _currentMoneyAmountText.text = _moneyManager.CurrentMoneyAmount + DOLLAR_SIGN;
}
