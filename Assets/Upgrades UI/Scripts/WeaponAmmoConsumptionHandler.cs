using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AmmoRestorationManager;

public class WeaponAmmoConsumptionHandler : MonoBehaviour
{
    public Action OnChangeRestorationType;

    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;

    private void Awake()
    {
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();

        _weaponAmmoConsumptionManagers = new List<WeaponAmmoConsumptionManager>();
    }

    public void ChangeRestorationType(string ammoRestorationTypeString)
    {
        AmmoRestorationType ammoRestorationType = (AmmoRestorationType)Enum.Parse(typeof(AmmoRestorationType), ammoRestorationTypeString);

        WeaponAmmoConsumptionManager weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
            .Find(manager => manager.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType);

        if (weaponAmmoConsumptionManager == null)
        {
            weaponAmmoConsumptionManager = GetComponentsInChildren<WeaponAmmoConsumptionManager>()
                .First(manager => manager.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType);

            _weaponAmmoConsumptionManagers.Add(weaponAmmoConsumptionManager);
        }

        weaponAmmoConsumptionManager.RestorationType = ammoRestorationType;
        OnChangeRestorationType?.Invoke();
    }
}
