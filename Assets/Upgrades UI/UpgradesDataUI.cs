using TMPro;
using UnityEngine;

public class UpgradesDataUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageInputField;
    [SerializeField] private TMP_Text _firingSpeedInputField;
    [SerializeField] private TMP_Text _costInputField;
    [SerializeField] private TMP_Text _movementSpeedInputField;

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
        _damageInputField.text = _currentDataManager.CurrentDamage.ToString();
        _firingSpeedInputField.text = _currentDataManager.CurrentFiringSpeed.ToString();
        _costInputField.text = _currentDataManager.CurrentCost.ToString();
        _movementSpeedInputField.text = _currentDataManager.CurrentMovementSpeed.ToString();
    }
}
