using TMPro;
using UnityEngine;

public class UpgradesDataUI : MonoBehaviour
{
    [Header("Curent Values")]
    [SerializeField] private TMP_Text _currentDamageInputField;
    [SerializeField] private TMP_Text _currentFiringSpeedInputField;
    [SerializeField] private TMP_Text _currentCostInputField;
    [SerializeField] private TMP_Text _currentMovementSpeedInputField;
    [Header("Upgraded Values")]
    [SerializeField] private TMP_Text _upgradedDamageInputField;
    [SerializeField] private TMP_Text _upgradedFiringSpeedInputField;
    [SerializeField] private TMP_Text _upgradedCostInputField;
    [SerializeField] private TMP_Text _upgradedMovementSpeedInputField;

    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private WeaponDataManager _currentDataManager;

    private void Awake()
    {
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _weaponUpgradeHandler.OnUpdateWeapon += UpdateData;
        UpdateData();
    }

    private void OnDestroy() => _weaponUpgradeHandler.OnUpdateWeapon -= UpdateData;

    private void UpdateData()
    {
        _currentDataManager = _weaponUpgradeHandler.CurrentDataManager;
        _currentDamageInputField.text = _currentDataManager.CurrentDamage.ToString("0.00");
        _currentFiringSpeedInputField.text = _currentDataManager.CurrentFiringSpeed.ToString("0.00");
        _currentCostInputField.text = _currentDataManager.CurrentCost.ToString("0.00");
        _currentMovementSpeedInputField.text = _currentDataManager.CurrentMovementSpeed.ToString("0.00");

        _upgradedDamageInputField.text = (_currentDataManager.CurrentDamage
            + _currentDataManager.StartingData.damageUpgradeStep).ToString("0.00");
        _upgradedFiringSpeedInputField.text = (_currentDataManager.CurrentFiringSpeed
            + _currentDataManager.StartingData.firingSpeedUpgradeStep).ToString("0.00");
        _upgradedCostInputField.text = (_currentDataManager.CurrentCost
            + _currentDataManager.StartingData.costUpgradeStep).ToString("0.00");
        _upgradedMovementSpeedInputField.text = (_currentDataManager.CurrentMovementSpeed
            + _currentDataManager.StartingData.movementSpeedUpgradeStep).ToString("0.00");
    }
}
