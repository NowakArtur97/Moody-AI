using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ProjectileObjectPool;

public class WeaponAmmoBarsManager : MonoBehaviour
{
    [SerializeField] private WeaponAmmoBarUI[] _ammoBars;
    [SerializeField] private WeaponAmmoBarImageUI[] _ammoBarsImages;

    private List<WeaponAmmoBarUI> _weaponAmmoBars;
    private List<WeaponAmmoBarImageUI> _weaponAmmoBarsImages;
    private WaveManager _waveManager;
    private WaveSpawner _waveSpawner;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;
    private List<WeaponUpgradeManager> _weaponUpgradeManagers;

    private void Awake()
    {
        _weaponAmmoBars = _ammoBars.ToList();
        _weaponAmmoBarsImages = _ammoBarsImages.ToList();

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

        _weaponAmmoBars
            .ForEach(bar =>
                {
                    WeaponAmmoConsumptionManager _weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
                        .Find(manager => manager.RestorationType == bar.RestorationType);
                    bar.gameObject.SetActive(_weaponAmmoConsumptionManager != null &&
                        activeProjectileTypes.Contains(_weaponAmmoConsumptionManager.ProjectileType));
                }
            );

        _weaponAmmoBarsImages
            .ForEach(image =>
                {
                    WeaponAmmoConsumptionManager _weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
                        .Find(manager => manager.RestorationType == image.RestorationType);
                    image.gameObject.SetActive(_weaponAmmoConsumptionManager != null &&
                        activeProjectileTypes.Contains(_weaponAmmoConsumptionManager.ProjectileType));
                }
            );
    }

    private void HideBars()
    {
        _weaponAmmoBars.ForEach(bar => bar.gameObject.SetActive(false));
        _weaponAmmoBarsImages.ForEach(bar => bar.gameObject.SetActive(false));
    }
}
