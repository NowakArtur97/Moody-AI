using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoRestorationManager : MonoBehaviour
{
    [SerializeField] private AmmoRecovery[] _ammoRecoveryData;
    public AmmoRecovery[] AmmoRecoveryData
    {
        get { return _ammoRecoveryData; }
        set { _ammoRecoveryData = value; }
    }

    public enum AmmoRestorationType { DEFEATING_ENEMIES, DAMAGING_PLANET, TAKING_DAMAGE, DODGING_BULLETS }
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
                .FirstOrDefault(manager => manager.RestorationType == ammoRestorationType);

            if (weaponAmmoConsumptionManager == null)
            {
                return;
            }

            _weaponAmmoConsumptionManager.Add(weaponAmmoConsumptionManager);
        }

        AmmoRecovery ammoRecovery = _ammoRecoveryData
            .First(recoveryData => recoveryData.RestorationType == ammoRestorationType);

        weaponAmmoConsumptionManager.RestoreAmmunition(ammoRecovery.RecoveryValue);
    }
}