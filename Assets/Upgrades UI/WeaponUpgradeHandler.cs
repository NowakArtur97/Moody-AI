using System;
using UnityEngine;

public class WeaponUpgradeHandler : MonoBehaviour
{
    public Action OnUpdateWeapon;

    [SerializeField] private WeaponUpgradeManager _currentWeaponUpgradeManager;
    public WeaponUpgradeManager CurrentWeaponUpgradeManager
    {
        get { return _currentWeaponUpgradeManager; }
        set { _currentWeaponUpgradeManager = value; }
    }

    public void ChangeDataManger(WeaponUpgradeManager weaponUpgradeManager)
    {
        _currentWeaponUpgradeManager = weaponUpgradeManager;
        OnUpdateWeapon?.Invoke();
    }

    public void UnlockWeapon()
    {
        _currentWeaponUpgradeManager.UnlockWeapon();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeDamage()
    {
        _currentWeaponUpgradeManager.UpgradeDamage();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeFiringSpeed()
    {
        _currentWeaponUpgradeManager.UpgradeFiringSpeed();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeAmmoConsumption()
    {
        _currentWeaponUpgradeManager.UpgradeAmmoConsumption();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeMovementSpeed()
    {
        _currentWeaponUpgradeManager.UpgradeMovementSpeed();
        OnUpdateWeapon?.Invoke();
    }
}
