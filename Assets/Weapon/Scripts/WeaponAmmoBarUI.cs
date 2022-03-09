using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AmmoRestorationManager;

public class WeaponAmmoBarUI : MonoBehaviour
{
    [SerializeField] private AmmoRestorationType _ammoRestorationType;
    public AmmoRestorationType RestorationType
    {
        get { return _ammoRestorationType; }
        set { _ammoRestorationType = value; }
    }

    private Scrollbar _myScrollbar;
    private WeaponAmmoConsumptionManager _weaponAmmoConsumptionManager;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;

    private void Awake()
    {
        _myScrollbar = GetComponent<Scrollbar>();

        _weaponAmmoConsumptionManagers = FindObjectsOfType<WeaponAmmoConsumptionManager>()
              .ToList();
    }

    private void OnEnable() => _weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
            .Find(manager => manager.RestorationType == _ammoRestorationType);

    private void Update()
    {
        if (_weaponAmmoConsumptionManager != null)
        {
            _myScrollbar.size = _weaponAmmoConsumptionManager.CurrentAmmoCapacity / _weaponAmmoConsumptionManager.MaxAmmoCapacity;
        }
    }
}
