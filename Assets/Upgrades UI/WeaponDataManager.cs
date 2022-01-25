using UnityEngine;
using static ProjectileObjectPool;

public class WeaponDataManager : MonoBehaviour
{
    [SerializeField] private D_WeaponUpgrade _upgradesData;
    public D_WeaponUpgrade UpgradesData
    {
        get { return _upgradesData; }
        set { _upgradesData = value; }
    }
    [SerializeField] private GameObject _weaponGameObject;
    private WeaponSystem _weaponSystem;

    private ProjectileType _projetileType;
    public float CurrentDamage { get; private set; }
    public float CurrentFiringSpeed { get; private set; }
    public float CurrentCost { get; private set; }
    public float CurrentMovementSpeed { get; private set; }
    public bool IsUnlocked { get; private set; }

    private void Awake()
    {
        _projetileType = _upgradesData.projetileType;
        CurrentDamage = _upgradesData.startingDamage;
        CurrentFiringSpeed = _upgradesData.startingFiringSpeed;
        CurrentCost = _upgradesData.startingCost;
        CurrentMovementSpeed = _upgradesData.startingMovementSpeed;
        IsUnlocked = _upgradesData.isUnlockedAtTheBegining;

        _weaponSystem = FindObjectOfType<WeaponSystem>();

        if (IsUnlocked)
        {
            UnlockWeapon();
        }
    }

    public void UnlockWeapon()
    {
        IsUnlocked = true;
        _weaponGameObject.SetActive(true);
        _weaponSystem.AddWeapon(_weaponGameObject);
    }

    public void UpgradeDamage() => CurrentDamage += _upgradesData.damageUpgradeStep;

    public void UpgradeFiringSpeed() => CurrentFiringSpeed += _upgradesData.firingSpeedUpgradeStep;

    public void UpgradeCost() => CurrentCost += _upgradesData.costUpgradeStep;

    public void UpgradeMovementSpeed() => CurrentMovementSpeed += _upgradesData.movementSpeedUpgradeStep;
}
