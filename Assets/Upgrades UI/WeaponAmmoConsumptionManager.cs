using System.Collections;
using System.Linq;
using UnityEngine;
using static ProjectileObjectPool;

public class WeaponAmmoConsumptionManager : MonoBehaviour
{
    [SerializeField] private ProjectileType _projectileType;
    public ProjectileType ProjectileType
    {
        get { return _projectileType; }
        set { _projectileType = value; }
    }
    [SerializeField] private float _ammunitionRecoveryTime = 1f;
    [SerializeField] private float _ammunitionRecoveryValue = 10f;
    [SerializeField] private float _maxAmmoCapacity = 400.0f;

    private WeaponDataManager _weaponDataManager;
    private bool _shouldRestore;

    public float CurrentAmmoCapacity { get; private set; }

    private void Awake() => _shouldRestore = true;

    private void Start()
    {
        _weaponDataManager = FindObjectsOfType<WeaponDataManager>().First(wdm => wdm.ProjectileType == _projectileType && wdm.IsEnemy == false);
        CurrentAmmoCapacity = _weaponDataManager.WeaponData.startingAmmoCapacity;
    }

    private void Update()
    {
        if (_shouldRestore)
        {
            StartCoroutine(AmmoRestorationCoroutine());
        }
        Debug.Log(CanShoot());
    }

    private IEnumerator AmmoRestorationCoroutine()
    {
        if (CurrentAmmoCapacity < _maxAmmoCapacity)
        {
            CurrentAmmoCapacity += _ammunitionRecoveryValue;
        }

        _shouldRestore = false;

        yield return new WaitForSeconds(_ammunitionRecoveryTime);

        _shouldRestore = true;
    }

    public bool CanShoot() => CurrentAmmoCapacity - _weaponDataManager.CurrentAmmoConsumption >= 0;

    public void ConsumeAmmunition() => CurrentAmmoCapacity -= _weaponDataManager.CurrentAmmoConsumption;
}
