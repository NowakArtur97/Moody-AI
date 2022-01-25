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
    private WeaponDataManager _currentDataManager;

    private void Awake()
    {
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _weaponUpgradeHandler.OnUpdateWeapon += UpdateUI;
    }

    private void Start() => UpdateUI();

    private void OnDestroy() => _weaponUpgradeHandler.OnUpdateWeapon -= UpdateUI;

    private void UpdateUI()
    {
        _currentDataManager = _weaponUpgradeHandler.CurrentDataManager;

        bool isUnlocked = _weaponUpgradeHandler.CurrentDataManager.IsUnlocked;
        UpgradesUI.gameObject.SetActive(isUnlocked);
        UnlockButton.gameObject.SetActive(!isUnlocked);

        _currentDamageInputField.text = _currentDataManager.CurrentDamage.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentFiringSpeedInputField.text = _currentDataManager.CurrentFiringSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentCostInputField.text = _currentDataManager.CurrentAmmoConsumption.ToString(TWO_DECIMAL_PLACES_FORMAT);
        _currentMovementSpeedInputField.text = _currentDataManager.CurrentMovementSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);

        float upgradedDamage = _currentDataManager.CurrentDamage + _currentDataManager.UpgradesData.damageUpgradeStep;
        _upgradedDamageInputField.text = upgradedDamage.ToString(TWO_DECIMAL_PLACES_FORMAT);
        float upgradedFiringSpeed = _currentDataManager.CurrentFiringSpeed + _currentDataManager.UpgradesData.firingSpeedUpgradeStep;
        _upgradedFiringSpeedInputField.text = upgradedFiringSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);
        float upgradedCost = _currentDataManager.CurrentAmmoConsumption + _currentDataManager.UpgradesData.ammoConsumptionUpgradeStep;
        _upgradedCostInputField.text = upgradedCost.ToString(TWO_DECIMAL_PLACES_FORMAT);
        float upgradedMovementSpeed = _currentDataManager.CurrentMovementSpeed + _currentDataManager.UpgradesData.movementSpeedUpgradeStep;
        _upgradedMovementSpeedInputField.text = upgradedMovementSpeed.ToString(TWO_DECIMAL_PLACES_FORMAT);

        _damageUpgradeButton.interactable = upgradedDamage < _currentDataManager.UpgradesData.maximallyUpgradedDamage;
        _firingSpeedUpgradeButton.interactable = upgradedFiringSpeed > _currentDataManager.UpgradesData.maximallyUpgradedFiringSpeed;
        _costUpgradeButton.interactable = upgradedCost > _currentDataManager.UpgradesData.maximallyUpgradedAmmoConsumption;
        _movementSpeedUpgradeButton.interactable = upgradedMovementSpeed < _currentDataManager.UpgradesData.maximallyUpgradedMovementSpeed;
    }
}
