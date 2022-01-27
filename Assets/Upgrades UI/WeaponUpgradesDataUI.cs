using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradesDataUI : MonoBehaviour
{
    private const string TWO_DECIMAL_PLACES_FORMAT = "0.00";

    [Header("General")]
    [SerializeField] private GameObject UpgradesUI;
    [SerializeField] private GameObject UnlockButton;

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

    [Header("Buttons")]
    [SerializeField] private Button _damageUpgradeButton;
    [SerializeField] private Button _firingSpeedUpgradeButton;
    [SerializeField] private Button _costUpgradeButton;
    [SerializeField] private Button _movementSpeedUpgradeButton;

    private WeaponUpgradeHandler _weaponUpgradeHandler;

    private void Awake()
    {
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _weaponUpgradeHandler.OnUpdateWeapon += UpdateUI;
    }

    private void Start() => UpdateUI();

    private void OnDestroy() => _weaponUpgradeHandler.OnUpdateWeapon -= UpdateUI;

    private void UpdateUI()
    {
        WeaponDataManager currentDataManager = FindObjectsOfType<WeaponDataManager>()
            .First(wdm => wdm.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType && wdm.IsEnemy == false);
        WeaponUpgradeManager weaponUpgradeDataManager = FindObjectsOfType<WeaponUpgradeManager>()
            .First(wdm => wdm.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType);

        bool isUnlocked = _weaponUpgradeHandler.CurrentWeaponUpgradeManager.IsUnlocked;
        UpgradesUI.gameObject.SetActive(isUnlocked);
        UnlockButton.gameObject.SetActive(!isUnlocked);

        _currentDamageInputField.text = currentDataManager.CurrentDamage.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentFiringSpeedInputField.text = currentDataManager.CurrentFiringSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentCostInputField.text = currentDataManager.CurrentAmmoConsumption.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentMovementSpeedInputField.text = currentDataManager.CurrentMovementSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);

        float upgradedDamage = currentDataManager.CurrentDamage + weaponUpgradeDataManager.WeaponUpgradeData.damageUpgradeStep;
        _upgradedDamageInputField.text = upgradedDamage.ToString(TWO_DECIMAL_PLACES_FORMAT);
        float upgradedFiringSpeed = currentDataManager.CurrentFiringSpeed + weaponUpgradeDataManager.WeaponUpgradeData.firingSpeedUpgradeStep;
        _upgradedFiringSpeedInputField.text = upgradedFiringSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);
        float upgradedCost = currentDataManager.CurrentAmmoConsumption + weaponUpgradeDataManager.WeaponUpgradeData.ammoConsumptionUpgradeStep;
        _upgradedCostInputField.text = upgradedCost.ToString(TWO_DECIMAL_PLACES_FORMAT);
        float upgradedMovementSpeed = currentDataManager.CurrentMovementSpeed + weaponUpgradeDataManager.WeaponUpgradeData.movementSpeedUpgradeStep;
        _upgradedMovementSpeedInputField.text = upgradedMovementSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);

        _damageUpgradeButton.interactable = upgradedDamage < weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedDamage;
        _firingSpeedUpgradeButton.interactable = upgradedFiringSpeed > weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedFiringSpeed;
        _costUpgradeButton.interactable = upgradedCost > weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedAmmoConsumption;
        _movementSpeedUpgradeButton.interactable = upgradedMovementSpeed < weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedMovementSpeed;
    }
}
