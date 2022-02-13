using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AmmoRestorationManager;

public class WeaponAmmoBarUI : MonoBehaviour
{
    [SerializeField] private AmmoRestorationType _ammoRestorationType;

    private Scrollbar _myScrollbar;
    private WeaponAmmoConsumptionManager _weaponAmmoConsumptionManager;
    private WaveManager _waveManager;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;

    private void Awake()
    {
        _myScrollbar = GetComponent<Scrollbar>();

        _weaponAmmoConsumptionManagers = FindObjectsOfType<WeaponAmmoConsumptionManager>()
              .ToList();

        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += FindWeaponAmmoConsumptionManager;
    }

    private void OnDestroy() => _waveManager.OnStartWave -= FindWeaponAmmoConsumptionManager;

    private void FindWeaponAmmoConsumptionManager(int wave) => _weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
        .Find(manager => manager.RestorationType == _ammoRestorationType);

    private void Update()
    {
        if (_weaponAmmoConsumptionManager != null)
        {
            _myScrollbar.size = _weaponAmmoConsumptionManager.CurrentAmmoCapacity / _weaponAmmoConsumptionManager.MaxAmmoCapacity;
        }
    }
}
