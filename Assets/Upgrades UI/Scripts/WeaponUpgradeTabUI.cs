using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeTabUI : MonoBehaviour
{
    [SerializeField] GameObject _weaponSelectionMenu;
    [SerializeField] GameObject _weaponUpgradeMenu;
    [SerializeField] private GameObject UnlockButtonGameObject;
    [SerializeField] private Button UnlockButton;
    [SerializeField] private TMP_Text UnlockButtonText;

    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private MoneyManager _moneyManager;

    private void Awake()
    {
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void HandleUI()
    {
        WeaponUpgradeManager currentWeaponUpgradeManager = _weaponUpgradeHandler.CurrentWeaponUpgradeManager;
        bool isUnlocked = currentWeaponUpgradeManager.IsUnlocked;
        UnlockButtonGameObject.gameObject.SetActive(!isUnlocked);
        if (UnlockButtonGameObject.activeInHierarchy)
        {
            UnlockButtonText.text = currentWeaponUpgradeManager.WeaponUpgradeData.unlockCost.ToString();
            UnlockButton.interactable = _moneyManager.CurrentMoneyAmount >= currentWeaponUpgradeManager.WeaponUpgradeData.unlockCost;
        }

        if (isUnlocked)
        {
            _weaponSelectionMenu.SetActive(!_weaponSelectionMenu.activeInHierarchy);
            _weaponUpgradeMenu.SetActive(!_weaponUpgradeMenu.activeInHierarchy);
        }
    }
}
