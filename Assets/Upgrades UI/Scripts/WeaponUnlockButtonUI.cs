using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlockButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject _unlockButtonGameObject;
    [SerializeField] private GameObject _moodSelectionGameObject;
    [SerializeField] private Button _unlockButton;
    [SerializeField] private TMP_Text _unlockButtonText;

    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private MoneyManager _moneyManager;

    private void Awake()
    {
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void HandleUnlockButton()
    {
        WeaponUpgradeManager currentWeaponUpgradeManager = _weaponUpgradeHandler.CurrentWeaponUpgradeManager;
        bool isUnlocked = currentWeaponUpgradeManager.IsUnlocked;
        _unlockButtonGameObject.gameObject.SetActive(!isUnlocked);
        _moodSelectionGameObject.SetActive(isUnlocked);

        if (_unlockButtonGameObject.activeInHierarchy)
        {
            _unlockButtonText.text = $"{currentWeaponUpgradeManager.WeaponUpgradeData.unlockCost.ToString()}$";
            _unlockButton.interactable = _moneyManager.CurrentMoneyAmount >= currentWeaponUpgradeManager.WeaponUpgradeData.unlockCost;
        }
    }
}
