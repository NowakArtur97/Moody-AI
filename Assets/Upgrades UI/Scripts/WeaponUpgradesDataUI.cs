using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradesDataUI : MonoBehaviour
{
    private const string MAXIMALLY_UPGRADED_BUTTON_MESSAGE = "";
    private const string MAXIMALLY_UPGRADED_PROPERTY_MESSAGE = "MAX";
    private const string TWO_DECIMAL_PLACES_FORMAT = "0.00";

    [Header("General")]
    [SerializeField] private GameObject UpgradesUI;
    [SerializeField] private GameObject UnlockButtonGameObject;
    [SerializeField] private Button UnlockButton;
    [SerializeField] private TMP_Text UnlockButtonText;

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
    [SerializeField] private TMP_Text _damageUpgradeButtonText;
    [SerializeField] private Button _firingSpeedUpgradeButton;
    [SerializeField] private TMP_Text _firingSpeedUpgradeButtonText;
    [SerializeField] private Button _costUpgradeButton;
    [SerializeField] private TMP_Text _costUpgradeButtonText;
    [SerializeField] private Button _movementSpeedUpgradeButton;
    [SerializeField] private TMP_Text _movementSpeedUpgradeButtonText;

    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private MoneyManager _moneyManager;

    private void Awake()
    {
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>(true);
        _weaponUpgradeHandler.OnUpdateWeapon += UpdateUI;

        _moneyManager = FindObjectOfType<MoneyManager>();
        _moneyManager.OnChangeMoneyAmount += UpdateUI;
    }

    private void Start() => UpdateUI();

    private void OnDestroy()
    {
        _weaponUpgradeHandler.OnUpdateWeapon -= UpdateUI;
        _moneyManager.OnChangeMoneyAmount -= UpdateUI;
    }

    private void UpdateUI()
    {
        WeaponDataManager currentDataManager = FindObjectsOfType<WeaponDataManager>()
            .First(wdm => wdm.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType && wdm.IsEnemy == false);
        WeaponUpgradeManager weaponUpgradeDataManager = FindObjectsOfType<WeaponUpgradeManager>()
            .First(wdm => wdm.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType);

        float currentMoneyAmount = _moneyManager.CurrentMoneyAmount;

        bool isUnlocked = _weaponUpgradeHandler.CurrentWeaponUpgradeManager.IsUnlocked;
        UpgradesUI.gameObject.SetActive(isUnlocked);
        UnlockButtonGameObject.gameObject.SetActive(!isUnlocked);
        if (UnlockButtonGameObject.activeInHierarchy)
        {
            UnlockButtonText.text = weaponUpgradeDataManager.WeaponUpgradeData.unlockCost.ToString();
            UnlockButton.interactable = currentMoneyAmount >= weaponUpgradeDataManager.WeaponUpgradeData.unlockCost;
        }

        _currentDamageInputField.text = currentDataManager.CurrentDamage.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentFiringSpeedInputField.text = currentDataManager.CurrentFiringSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentCostInputField.text = currentDataManager.CurrentAmmoConsumption.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentMovementSpeedInputField.text = currentDataManager.CurrentMovementSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);

        float upgradedDamage = currentDataManager.CurrentDamage + weaponUpgradeDataManager.WeaponUpgradeData.damageUpgradeStep;
        float upgradedFiringSpeed = currentDataManager.CurrentFiringSpeed + weaponUpgradeDataManager.WeaponUpgradeData.firingSpeedUpgradeStep;
        float upgradedAmmoConsumption = currentDataManager.CurrentAmmoConsumption + weaponUpgradeDataManager.WeaponUpgradeData.ammoConsumptionUpgradeStep;
        float upgradedMovementSpeed = currentDataManager.CurrentMovementSpeed + weaponUpgradeDataManager.WeaponUpgradeData.movementSpeedUpgradeStep;

        bool isDamageUpgradable = upgradedDamage < weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedDamage;
        bool isFiringSpeedUpgradable = upgradedFiringSpeed > weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedFiringSpeed;
        bool isAmmoConsumptionUpgradable = upgradedAmmoConsumption > weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedAmmoConsumption;
        bool isMovementSpeedUpgradable = upgradedMovementSpeed < weaponUpgradeDataManager.WeaponUpgradeData.maximallyUpgradedMovementSpeed;

        _damageUpgradeButtonText.text = isDamageUpgradable ? weaponUpgradeDataManager.CurrentDamageCost.ToString()
            : MAXIMALLY_UPGRADED_BUTTON_MESSAGE;
        _damageUpgradeButton.interactable = isDamageUpgradable
            && currentMoneyAmount >= weaponUpgradeDataManager.CurrentDamageCost;
        _firingSpeedUpgradeButtonText.text = isFiringSpeedUpgradable ? weaponUpgradeDataManager.CurrentFiringSpeedCost.ToString()
            : MAXIMALLY_UPGRADED_BUTTON_MESSAGE;
        _firingSpeedUpgradeButton.interactable = isFiringSpeedUpgradable
            && currentMoneyAmount >= weaponUpgradeDataManager.CurrentFiringSpeedCost;
        _costUpgradeButtonText.text = isAmmoConsumptionUpgradable ? weaponUpgradeDataManager.CurrentAmmoConsumptionCost.ToString()
            : MAXIMALLY_UPGRADED_BUTTON_MESSAGE;
        _costUpgradeButton.interactable = isAmmoConsumptionUpgradable
            && currentMoneyAmount >= weaponUpgradeDataManager.CurrentAmmoConsumptionCost;
        _movementSpeedUpgradeButtonText.text = isMovementSpeedUpgradable ? weaponUpgradeDataManager.CurrentMovementSpeedCost.ToString()
            : MAXIMALLY_UPGRADED_BUTTON_MESSAGE;
        _movementSpeedUpgradeButton.interactable = isMovementSpeedUpgradable
            && currentMoneyAmount >= weaponUpgradeDataManager.CurrentMovementSpeedCost;

        _upgradedDamageInputField.text = isDamageUpgradable ? upgradedDamage.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
        _upgradedFiringSpeedInputField.text = isFiringSpeedUpgradable ? upgradedFiringSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
        _upgradedCostInputField.text = isAmmoConsumptionUpgradable ? upgradedAmmoConsumption.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
        _upgradedMovementSpeedInputField.text = isMovementSpeedUpgradable ? upgradedMovementSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT)
            : MAXIMALLY_UPGRADED_PROPERTY_MESSAGE;
    }
}
