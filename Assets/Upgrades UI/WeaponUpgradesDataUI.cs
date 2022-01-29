using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradesDataUI : MonoBehaviour
{
    private const string MAXIMALLY_UPGRADED_PROPERTY_MESSAGE = "MAX";
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
        float upgradedFiringSpeed = currentDataManager.CurrentFiringSpeed + weaponUpgradeDataManager.WeaponUpgradeData.firingSpeedUpgradeStep;
        float upgradedCost = currentDataManager.CurrentAmmoConsumption + weaponUpgradeDataManager.WeaponUpgradeData.ammoConsumptionUpgradeStep;
        float upgradedMovementSpeed = currentDataManager.CurrentMovementSpeed + weaponUpgradeDataManager.WeaponUpgradeData.movementSpeedUpgradeStep;

        bool isDamagaeMaximallyUpgraded = upgradedDamage < weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedDamage;
        bool isFiringSpeedMaximallyUpgraded = upgradedFiringSpeed > weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedFiringSpeed;
        bool isAmmoConsuptionMaximallyUpgraded = upgradedCost > weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedAmmoConsumption;
        bool isMovementSpeedMaximallyUpgraded = upgradedMovementSpeed < weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedMovementSpeed;

        _damageUpgradeButton.interactable = isDamagaeMaximallyUpgraded;
        _firingSpeedUpgradeButton.interactable = isFiringSpeedMaximallyUpgraded;
        _costUpgradeButton.interactable = isAmmoConsuptionMaximallyUpgraded;
        _movementSpeedUpgradeButton.interactable = isMovementSpeedMaximallyUpgraded;

        _upgradedDamageInputField.text = isDamagaeMaximallyUpgraded ? upgradedDamage.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
        _upgradedFiringSpeedInputField.text = isFiringSpeedMaximallyUpgraded ? upgradedFiringSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
        _upgradedCostInputField.text = isAmmoConsuptionMaximallyUpgraded ? upgradedCost.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
        _upgradedMovementSpeedInputField.text = isAmmoConsuptionMaximallyUpgraded ? upgradedMovementSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
    }
}
