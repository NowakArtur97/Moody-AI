using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AmmoRestorationManager;

public class WeaponAmmoBarsManager : MonoBehaviour
{
    [SerializeField] private WeaponAmmoBarUI[] _ammoBars;

    private List<WeaponAmmoBarUI> _weaponAmmoBars;
    private WaveManager _waveManager;
    private WaveSpawner _waveSpawner;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;

    private void Awake()
    {
        _weaponAmmoBars = _ammoBars.ToList();

        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += ShowUsedBars;

        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += HideBars;

        _weaponAmmoConsumptionManagers = FindObjectsOfType<WeaponAmmoConsumptionManager>().ToList();
    }

    private void OnDestroy()
    {
        _waveManager.OnStartWave -= ShowUsedBars;
        _waveSpawner.OnFinishWave -= HideBars;
    }

    private void ShowUsedBars(int waveNumber) =>
        _weaponAmmoBars
            .ForEach(bar => bar.gameObject
                .SetActive(_weaponAmmoConsumptionManagers.Exists(manager => manager.RestorationType == bar.RestorationType)));

    private void HideBars() => _weaponAmmoBars.ForEach(bar => bar.gameObject.SetActive(false));
}
