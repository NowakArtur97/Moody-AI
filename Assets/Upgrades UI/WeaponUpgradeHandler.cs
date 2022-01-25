using System;
using UnityEngine;

public class WeaponUpgradeHandler : MonoBehaviour
{
    public Action OnUpdateWeapon;

    [SerializeField] private WeaponDataManager _currentDataManager;
    public WeaponDataManager CurrentDataManager
    {
        get { return _currentDataManager; }
        set { _currentDataManager = value; }
    }

    public void ChangeDataManger(WeaponDataManager dataManager)
    {
        _currentDataManager = dataManager;
        OnUpdateWeapon?.Invoke();
    }

    public void UnlockWeapon()
    {
        _currentDataManager.UnlockWeapon();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeDamage()
    {
        _currentDataManager.UpgradeDamage();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeFiringSpeed()
    {
        _currentDataManager.UpgradeFiringSpeed();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeAmmoConsumption()
    {
        _currentDataManager.UpgradeAmmoConsumption();
        OnUpdateWeapon?.Invoke();
    }

    public void UpgradeMovementSpeed()
    {
        _currentDataManager.UpgradeMovementSpeed();
        OnUpdateWeapon?.Invoke();
    }
}
