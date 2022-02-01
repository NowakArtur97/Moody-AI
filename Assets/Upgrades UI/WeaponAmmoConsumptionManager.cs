using System.Linq;
using UnityEngine;
using static AmmoRestorationManager;
using static ProjectileObjectPool;

public class WeaponAmmoConsumptionManager : MonoBehaviour
{
    [SerializeField] private ProjectileType _projectileType;
    public ProjectileType ProjectileType
    {
        get { return _projectileType; }
        set { _projectileType = value; }
    }
    [SerializeField] private AmmoRestorationType _restorationType;
    public AmmoRestorationType RestorationType
    {
        get { return _restorationType; }
        set { _restorationType = value; }
    }
    [SerializeField] private float _maxAmmoCapacity = 400.0f;

    private WeaponDataManager _weaponDataManager;

    public float CurrentAmmoCapacity { get; private set; }

    private void Start()
    {
        _weaponDataManager = FindObjectsOfType<WeaponDataManager>().First(wdm => wdm.ProjectileType == _projectileType && wdm.IsEnemy == false);
        CurrentAmmoCapacity = _weaponDataManager.WeaponData.startingAmmoCapacity;
    }

    public void RestoreAmmunition(float _ammunitionRecoveryValue)
    {
        if (CurrentAmmoCapacity < _maxAmmoCapacity)
        {
            CurrentAmmoCapacity += _ammunitionRecoveryValue;
        }
    }

    public bool CanShoot() => CurrentAmmoCapacity - _weaponDataManager.CurrentAmmoConsumption >= 0;

    public void ConsumeAmmunition() => CurrentAmmoCapacity -= _weaponDataManager.CurrentAmmoConsumption;
}
