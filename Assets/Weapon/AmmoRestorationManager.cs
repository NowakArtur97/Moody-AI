using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoRestorationManager : MonoBehaviour
{
    [SerializeField] private AmmoRecovery[] _ammoRecoveryData;

    public enum AmmoRestorationType { DEFEATING_ENEMIES }
    public static AmmoRestorationManager AmmoRestorationManagerInstance { get; private set; }

    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManager;

    private void Awake()
    {
        if (AmmoRestorationManagerInstance != null && AmmoRestorationManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            AmmoRestorationManagerInstance = this;
        }

        _weaponAmmoConsumptionManager = new List<WeaponAmmoConsumptionManager>();
    }

    public void RestoreAmmunition(AmmoRestorationType ammoRestorationType)
    {
        WeaponAmmoConsumptionManager weaponAmmoConsumptionManager = _weaponAmmoConsumptionManager
            .Find(manager => manager.RestorationType == ammoRestorationType);

        if (weaponAmmoConsumptionManager == null)
        {
            weaponAmmoConsumptionManager = FindObjectsOfType<WeaponAmmoConsumptionManager>()
                .First(manager => manager.RestorationType == ammoRestorationType);

            _weaponAmmoConsumptionManager.Add(weaponAmmoConsumptionManager);
        }

        AmmoRecovery ammoRecovery = _ammoRecoveryData
            .First(recoveryData => recoveryData.RestorationType == ammoRestorationType);

        weaponAmmoConsumptionManager.RestoreAmmunition(ammoRecovery.RecoveryValue);
    }
}
