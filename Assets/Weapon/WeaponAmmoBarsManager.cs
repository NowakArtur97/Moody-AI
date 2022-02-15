using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ProjectileObjectPool;

public class WeaponAmmoBarsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _barsWrappers;

    private List<GameObject> _ammoBarsWrappers;
    private WaveManager _waveManager;
    private WaveSpawner _waveSpawner;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;
    private List<WeaponUpgradeManager> _weaponUpgradeManagers;

    private void Awake()
    {
        _ammoBarsWrappers = _barsWrappers.ToList();

        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += ShowUsedBars;

        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += HideBars;

        _weaponAmmoConsumptionManagers = FindObjectsOfType<WeaponAmmoConsumptionManager>().ToList();
        _weaponUpgradeManagers = FindObjectsOfType<WeaponUpgradeManager>().ToList();
    }

    private void OnDestroy()
    {
        _waveManager.OnStartWave -= ShowUsedBars;
        _waveSpawner.OnFinishWave -= HideBars;
    }

    private void ShowUsedBars(int waveNumber)
    {
        List<ProjectileType> activeProjectileTypes = _weaponUpgradeManagers
            .Where(manager => manager.IsUnlocked)
            .Select(manager => manager.ProjectileType)
            .ToList();

        _ammoBarsWrappers.ToList()
            .ForEach(bar =>
                {
                    WeaponAmmoConsumptionManager _weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
                        .Find(manager => manager.RestorationType
                                == bar.GetComponentInChildren<WeaponAmmoBarUI>().RestorationType);
                    bar.gameObject.SetActive(_weaponAmmoConsumptionManager != null &&
                        activeProjectileTypes.Contains(_weaponAmmoConsumptionManager.ProjectileType));
                }
            );
    }

    private void HideBars() => _ammoBarsWrappers.ToList().ForEach(bar => bar.gameObject.SetActive(false));
}
