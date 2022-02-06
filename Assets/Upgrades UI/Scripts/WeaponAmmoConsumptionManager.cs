using System.Collections;
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
    [SerializeField] private float _defaultRecoveryTime = 1f;
    [SerializeField] private float _defaultRecoveryValue = 5f;

    private WeaponDataManager _weaponDataManager;
    private bool _shouldRestore;

    public float CurrentAmmoCapacity { get; private set; }

    private void Awake() => _shouldRestore = true;

    private void Start()
    {
        _weaponDataManager = FindObjectsOfType<WeaponDataManager>()
            .First(wdm => wdm.ProjectileType == _projectileType && wdm.IsEnemy == false);
        CurrentAmmoCapacity = _weaponDataManager.WeaponData.startingAmmoCapacity;
    }

    private void Update()
    {
        if (_shouldRestore)
        {
            StartCoroutine(AmmoRestorationCoroutine());
        }
    }

    private IEnumerator AmmoRestorationCoroutine()
    {
        RestoreAmmunition(_defaultRecoveryValue);

        _shouldRestore = false;

        yield return new WaitForSeconds(_defaultRecoveryTime);

        _shouldRestore = true;
    }

    public void RestoreAmmunition(float _ammunitionRecoveryValue)
    {
        if (CurrentAmmoCapacity + _ammunitionRecoveryValue >= _maxAmmoCapacity)
        {
            CurrentAmmoCapacity = _maxAmmoCapacity;
        }
        else
        {
            CurrentAmmoCapacity += _ammunitionRecoveryValue;
        }
    }

    public bool CanShoot() => CurrentAmmoCapacity - _weaponDataManager.CurrentAmmoConsumption >= 0;

    public void ConsumeAmmunition() => CurrentAmmoCapacity -= _weaponDataManager.CurrentAmmoConsumption;
}
