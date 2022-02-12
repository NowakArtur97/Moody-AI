using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlockButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject UnlockButtonGameObject;
    [SerializeField] private GameObject MoodSelectionGameObject;
    [SerializeField] private Button UnlockButton;
    [SerializeField] private TMP_Text UnlockButtonText;

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
        UnlockButtonGameObject.gameObject.SetActive(!isUnlocked);
        MoodSelectionGameObject.SetActive(isUnlocked);

        if (UnlockButtonGameObject.activeInHierarchy)
        {
            UnlockButtonText.text = currentWeaponUpgradeManager.WeaponUpgradeData.unlockCost.ToString();
            UnlockButton.interactable = _moneyManager.CurrentMoneyAmount >= currentWeaponUpgradeManager.WeaponUpgradeData.unlockCost;
        }
    }
}
