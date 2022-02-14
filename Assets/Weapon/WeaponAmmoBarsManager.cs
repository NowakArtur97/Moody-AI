using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponAmmoBarsManager : MonoBehaviour
{
    [SerializeField] private WeaponAmmoBarUI[] _ammoBars;
    [SerializeField] private WeaponAmmoBarImageUI[] _ammoBarsImages;

    private List<WeaponAmmoBarUI> _weaponAmmoBars;
    private List<WeaponAmmoBarImageUI> _weaponAmmoBarsImages;
    private WaveManager _waveManager;
    private WaveSpawner _waveSpawner;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;

    private void Awake()
    {
        _weaponAmmoBars = _ammoBars.ToList();
        _weaponAmmoBarsImages = _ammoBarsImages.ToList();

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

    private void ShowUsedBars(int waveNumber)
    {
        _weaponAmmoBars
            .ForEach(bar => bar.gameObject
                .SetActive(_weaponAmmoConsumptionManagers.Exists(manager => manager.RestorationType == bar.RestorationType)));

        _weaponAmmoBarsImages
            .ForEach(image => image.gameObject
                .SetActive(_weaponAmmoConsumptionManagers.Exists(manager => manager.RestorationType == image.RestorationType)));
    }

    private void HideBars()
    {
        _weaponAmmoBars.ForEach(bar => bar.gameObject.SetActive(false));
        _weaponAmmoBarsImages.ForEach(bar => bar.gameObject.SetActive(false));
    }
}
