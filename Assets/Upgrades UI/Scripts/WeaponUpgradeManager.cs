using System.Linq;
using UnityEngine;
using static ProjectileObjectPool;

public class WeaponUpgradeManager : MonoBehaviour
{
    [SerializeField] private D_WeaponUpgrade _weaponUpgradeData;
    public D_WeaponUpgrade WeaponUpgradeData
    {
        get { return _weaponUpgradeData; }
        set { _weaponUpgradeData = value; }
    }
    [SerializeField] private GameObject _weaponPrefab;

    private WeaponSystem _weaponSystem;
    private MoneyManager _moneyManager;

    public ProjectileType ProjectileType { get; private set; }
    private WeaponDataManager _weaponDataManager;
    private PlayerLifeManager _playerLifeManager;

    public bool IsUnlocked { get; private set; }
    public int CurrentDamageCost { get; private set; }
    public int CurrentFiringSpeedCost { get; private set; }
    public int CurrentAmmoConsumptionCost { get; private set; }
    public int CurrentMovementSpeedCost { get; private set; }

    private void Awake()
    {
        ProjectileType = _weaponUpgradeData.projetileType;
        IsUnlocked = _weaponUpgradeData.isUnlockedAtTheBegining;

        _moneyManager = FindObjectOfType<MoneyManager>();

        _playerLifeManager = FindObjectOfType<PlayerLifeManager>();
        _playerLifeManager.OnPlayerRespawn += UnlockWeaponOrNot;
    }

    private void OnDestroy() => _playerLifeManager.OnPlayerRespawn -= UnlockWeaponOrNot;

    private void Start()
    {
        CurrentDamageCost = _weaponUpgradeData.startingDamageUpgradeCost;
        CurrentFiringSpeedCost = _weaponUpgradeData.startingFiringSpeedUpgradeCost;
        CurrentAmmoConsumptionCost = _weaponUpgradeData.startingAmmoConsumptionUpgradeCost;
        CurrentMovementSpeedCost = _weaponUpgradeData.startingMovementSpeedUpgradeCost;

        _weaponDataManager = FindObjectsOfType<WeaponDataManager>()
            .First(wdm => wdm.ProjectileType == ProjectileType && wdm.IsEnemy == false);
    }

    private void UnlockWeaponOrNot()
    {
        if (IsUnlocked || _weaponUpgradeData.isUnlockedAtTheBegining)
        {
            UnlockWeapon();
        }
    }

    public void UnlockWeapon()
    {
        if (!_weaponUpgradeData.isUnlockedAtTheBegining && !IsUnlocked)
        {
            _moneyManager.DecreaseMoneyAmount(_weaponUpgradeData.unlockCost);
        }

        IsUnlocked = true;

        _weaponSystem = FindObjectOfType<WeaponSystem>();

        GameObject weaponGameObject = Instantiate(_weaponPrefab, Vector2.zero, Quaternion.identity);
        weaponGameObject.transform.parent = _weaponSystem.transform;
        _weaponSystem.AddWeapon(weaponGameObject);
    }

    public void UpgradeDamage()
    {
        _moneyManager.DecreaseMoneyAmount(CurrentDamageCost);
        CurrentDamageCost += _weaponUpgradeData.damageUpgradeCostStep;
        _weaponDataManager.UpgradeDamage(_weaponUpgradeData.damageUpgradeStep);
    }

    public void UpgradeFiringSpeed()
    {
        _moneyManager.DecreaseMoneyAmount(CurrentFiringSpeedCost);
        CurrentFiringSpeedCost += _weaponUpgradeData.firingSpeedUpgradeCostStep;
        _weaponDataManager.UpgradeFiringSpeed(_weaponUpgradeData.firingSpeedUpgradeStep);
    }

    public void UpgradeAmmoConsumption()
    {
        _moneyManager.DecreaseMoneyAmount(CurrentAmmoConsumptionCost);
        CurrentAmmoConsumptionCost += _weaponUpgradeData.ammoConsumptionUpgradeCostStep;
        _weaponDataManager.UpgradeAmmoConsumption(_weaponUpgradeData.ammoConsumptionUpgradeStep);
    }

    public void UpgradeMovementSpeed()
    {
        _moneyManager.DecreaseMoneyAmount(CurrentMovementSpeedCost);
        CurrentMovementSpeedCost += _weaponUpgradeData.movementSpeedUpgradeCostStep;
        _weaponDataManager.UpgradeMovementSpeed(_weaponUpgradeData.movementSpeedUpgradeStep);
    }
}
