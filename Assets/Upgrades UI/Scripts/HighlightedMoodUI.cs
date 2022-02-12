using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AmmoRestorationManager;

public class HighlightedMoodUI : MonoBehaviour
{
    [SerializeField] private AmmoRestorationType _ammoRestorationType;

    private Button _myButton;
    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private WeaponAmmoConsumptionHandler _weaponAmmoConsumptionHandler;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;

    private void Awake()
    {
        _myButton = GetComponent<Button>();

        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _weaponUpgradeHandler.OnUpdateWeapon += HandleMoodHighlight;
        _weaponAmmoConsumptionHandler = FindObjectOfType<WeaponAmmoConsumptionHandler>();
        _weaponAmmoConsumptionHandler.OnChangeRestorationType += HandleMoodHighlight;

        _weaponAmmoConsumptionManagers = FindObjectsOfType<WeaponAmmoConsumptionManager>()
              .ToList();
    }

    private void OnDestroy()
    {
        _weaponUpgradeHandler.OnUpdateWeapon -= HandleMoodHighlight;
        _weaponAmmoConsumptionHandler.OnChangeRestorationType -= HandleMoodHighlight;
    }

    private void OnEnable() => HandleMoodHighlight();

    private void HandleMoodHighlight()
    {
        WeaponAmmoConsumptionManager weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
            .Find(manager => manager.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType);

        if (_ammoRestorationType == weaponAmmoConsumptionManager.RestorationType)
        {
            _myButton.Select();
        }
    }
}
