using UnityEngine;
using static ProjectileObjectPool;

public class WeaponDataManager : MonoBehaviour
{
    [SerializeField] private D_Weapon _weaponData;
    public D_Weapon WeaponData
    {
        get { return _weaponData; }
        set { _weaponData = value; }
    }
    [SerializeField] private bool _isEnemy;
    public bool IsEnemy
    {
        get { return _isEnemy; }
        set { _isEnemy = value; }
    }

    public ProjectileType ProjectileType { get; private set; }
    public float CurrentDamage { get; private set; }
    public float CurrentFiringSpeed { get; private set; }
    public float CurrentAmmoConsumption { get; private set; }
    public float CurrentMovementSpeed { get; private set; }

    private void Awake()
    {
        ProjectileType = _weaponData.projetileType;
        CurrentDamage = _weaponData.startingDamage;
        CurrentFiringSpeed = _weaponData.startingFiringSpeed;
        CurrentAmmoConsumption = _weaponData.startingAmmoConsumption;
        CurrentMovementSpeed = _weaponData.startingMovementSpeed;
    }

    public void UpgradeDamage(float damageUpgradeStep) => CurrentDamage += damageUpgradeStep;

    public void UpgradeFiringSpeed(float firingSpeedUpgradeStep) => CurrentFiringSpeed += firingSpeedUpgradeStep;

    public void UpgradeAmmoConsumption(float ammoConsumptionUpgradeStep) => CurrentAmmoConsumption += ammoConsumptionUpgradeStep;

    public void UpgradeMovementSpeed(float movementSpeedUpgradeStep) => CurrentMovementSpeed += movementSpeedUpgradeStep;
}
