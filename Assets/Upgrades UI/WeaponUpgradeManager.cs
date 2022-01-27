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

    public ProjectileType ProjectileType { get; private set; }
    private WeaponDataManager _weaponDataManager;

    public bool IsUnlocked { get; private set; }

    private void Awake()
    {
        ProjectileType = _weaponUpgradeData.projetileType;
        IsUnlocked = _weaponUpgradeData.isUnlockedAtTheBegining;

        _weaponSystem = FindObjectOfType<WeaponSystem>();

        if (IsUnlocked)
        {
            UnlockWeapon();
        }
    }

    private void Start() => _weaponDataManager = FindObjectsOfType<WeaponDataManager>()
        .First(wdm => wdm.ProjectileType == ProjectileType);

    public void UnlockWeapon()
    {
        IsUnlocked = true;
        _weaponGameObject.SetActive(true);
        _weaponSystem.AddWeapon(_weaponGameObject);
    }

    public void UpgradeDamage() => _weaponDataManager.UpgradeDamage(_weaponUpgradeData.damageUpgradeStep);

    public void UpgradeFiringSpeed() => _weaponDataManager.UpgradeFiringSpeed(_weaponUpgradeData.firingSpeedUpgradeStep);

    public void UpgradeAmmoConsumption() => _weaponDataManager.UpgradeAmmoConsumption(_weaponUpgradeData.ammoConsumptionUpgradeStep);

    public void UpgradeMovementSpeed() => _weaponDataManager.UpgradeMovementSpeed(_weaponUpgradeData.movementSpeedUpgradeStep);
}
