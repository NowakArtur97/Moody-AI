using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ProjectileObjectPool;

public class WeaponConsumptionManager : MonoBehaviour
{
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] private float _ammunitionRecoveryTime = 1f;
    [SerializeField] private float _ammunitionRecoveryValue = 10f;
    [SerializeField] private float _maxAmmoCapacity = 400.0f;

    private WeaponDataManager _weaponDataManager;
    private bool _shouldRestore;

    public float CurrentAmmoCapacity { get; private set; }

    private void Start() => _weaponDataManager = FindObjectsOfType<WeaponDataManager>().First(wdm => wdm.ProjectileType == _projectileType && wdm.IsEnemy == false);

    private void Update()
    {
        if (_shouldRestore)
        {
            StartCoroutine(AmmoRestorationCoroutine());
            Debug.Log(CurrentAmmoCapacity);
        }
    }

    private IEnumerator AmmoRestorationCoroutine()
    {
        _shouldRestore = false;

        if (CurrentAmmoCapacity < _maxAmmoCapacity)
        {
            CurrentAmmoCapacity += _ammunitionRecoveryValue;
        }

        yield return new WaitForSeconds(_ammunitionRecoveryTime);

        _shouldRestore = true;
    }

    public bool CanShoot() => CurrentAmmoCapacity >= _weaponDataManager.CurrentAmmoConsumption;

    public void ConsumeAmmunition() => CurrentAmmoCapacity -= _weaponDataManager.CurrentAmmoConsumption;
}
