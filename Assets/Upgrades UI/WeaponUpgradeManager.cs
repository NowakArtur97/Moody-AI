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
    [SerializeField] private GameObject _weaponGameObject;
    private WeaponSystem _weaponSystem;
    private MoneyManager _moneyManager;

    public ProjectileType ProjectileType { get; private set; }
    private WeaponDataManager _weaponDataManager;

    public bool IsUnlocked { get; private set; }
    public int CurrentDamageCost { get; private set; }
    public int CurrentFiringSpeedCost { get; private set; }
    public int CurrentAmmoConsumptionCost { get; private set; }
    public int CurrentMovementSpeedCost { get; private set; }

    private void Awake()
    {
        ProjectileType = _weaponUpgradeData.projetileType;
        IsUnlocked = _weaponUpgradeData.isUnlockedAtTheBegining;

        _weaponSystem = FindObjectOfType<WeaponSystem>();
        _moneyManager = FindObjectOfType<MoneyManager>();

        if (IsUnlocked)
        {
            UnlockWeapon();
        }

        CurrentDamageCost = _weaponUpgradeData.startingDamageUpgradeCost;
        CurrentFiringSpeedCost = _weaponUpgradeData.startingFiringSpeedUpgradeCost;
        CurrentAmmoConsumptionCost = _weaponUpgradeData.startingAmmoConsumptionUpgradeCost;
        CurrentMovementSpeedCost = _weaponUpgradeData.startingMovementSpeedUpgradeCost;
    }

    private void Start() => _weaponDataManager = FindObjectsOfType<WeaponDataManager>()
        .First(wdm => wdm.ProjectileType == ProjectileType && wdm.IsEnemy == false);

    public void UnlockWeapon()
    {
        IsUnlocked = true;
        _weaponGameObject.SetActive(true);
        _weaponSystem.AddWeapon(_weaponGameObject);
    }

    public void UpgradeDamage()
    {
        CurrentDamageCost += _weaponUpgradeData.damageUpgradeCostStep;
        _weaponDataManager.UpgradeDamage(_weaponUpgradeData.damageUpgradeStep);
        _moneyManager.DecreaseMoneyAmount(CurrentDamageCost);
    }

    public void UpgradeFiringSpeed()
    {
        CurrentFiringSpeedCost += _weaponUpgradeData.firingSpeedUpgradeCostStep;
        _weaponDataManager.UpgradeFiringSpeed(_weaponUpgradeData.firingSpeedUpgradeStep);
        _moneyManager.DecreaseMoneyAmount(CurrentFiringSpeedCost);
    }

    public void UpgradeAmmoConsumption()
    {
        CurrentAmmoConsumptionCost += _weaponUpgradeData.ammoConsumptionUpgradeCostStep;
        _weaponDataManager.UpgradeAmmoConsumption(_weaponUpgradeData.ammoConsumptionUpgradeStep);
        _moneyManager.DecreaseMoneyAmount(CurrentAmmoConsumptionCost);
    }

    public void UpgradeMovementSpeed()
    {
        CurrentMovementSpeedCost += _weaponUpgradeData.movementSpeedUpgradeCostStep;
        _weaponDataManager.UpgradeMovementSpeed(_weaponUpgradeData.movementSpeedUpgradeStep);
        _moneyManager.DecreaseMoneyAmount(CurrentMovementSpeedCost);
    }
}
