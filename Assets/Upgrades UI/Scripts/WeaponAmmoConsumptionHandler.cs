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
    private List<AmmoRestorationType> ammoRestorationTypeValues;

    private void Awake()
    {
        ammoRestorationTypeValues = Enum.GetValues(typeof(AmmoRestorationType)).Cast<AmmoRestorationType>()
            .Where(restorationType => restorationType != AmmoRestorationType.EMPTY)
            .ToList();

        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();

        _weaponAmmoConsumptionManagers = FindObjectsOfType<WeaponAmmoConsumptionManager>().ToList();

        _weaponUpgradeHandler.OnUpdateWeapon += SetupMoodsForAllEmptyManagers;

        SetupMoodsForAllEmptyManagers();
    }

    private void OnDestroy() => _weaponUpgradeHandler.OnUpdateWeapon -= SetupMoodsForAllEmptyManagers;

    public void ChangeRestorationType(string ammoRestorationTypeString)
    {
        AmmoRestorationType ammoRestorationType = (AmmoRestorationType)Enum.Parse(typeof(AmmoRestorationType), ammoRestorationTypeString);

        WeaponAmmoConsumptionManager weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
            .Find(manager => manager.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType);

        if (weaponAmmoConsumptionManager.RestorationType == ammoRestorationType)
        {
            return;
        }

        RemovePreviousIfExists(ammoRestorationType);

        weaponAmmoConsumptionManager.RestorationType = ammoRestorationType;

        SetupMoodsForAllEmptyManagers();

        OnChangeRestorationType?.Invoke();
    }

    private void SetupMoodsForAllEmptyManagers()
    {
        List<AmmoRestorationType> freeRestorationType = ammoRestorationTypeValues
               .FindAll(restorationType => !_weaponAmmoConsumptionManagers.Any(manager => manager.RestorationType == restorationType));

        int index = 0;

        _weaponAmmoConsumptionManagers.Where(manager => manager.RestorationType == AmmoRestorationType.EMPTY)
            .ToList()
            .ForEach(manager => manager.RestorationType = freeRestorationType[index++]);
    }

    private void RemovePreviousIfExists(AmmoRestorationType ammoRestorationType)
    {
        WeaponAmmoConsumptionManager weaponAmmoConsumptionManagerWithSelectedRestorationType = _weaponAmmoConsumptionManagers
                    .Find(manager => manager.RestorationType == ammoRestorationType);

        if (weaponAmmoConsumptionManagerWithSelectedRestorationType != null)
        {
            weaponAmmoConsumptionManagerWithSelectedRestorationType.RestorationType = AmmoRestorationType.EMPTY;
        }
    }
}
