using System.Collections;
using System.Linq;
using UnityEngine;
using static ProjectileObjectPool;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    private const string PLAYER_PROJECTILE_LAYER = "Player Projectile";
    private const string ENEMY_PROJECTILE_LAYER = "Enemy Projectile";

    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] Transform _projectileSpawnPosition;
    [SerializeField] float _minSoundPitch = 0.95f;
    [SerializeField] float _maxSoundPitch = 1.05f;
    [SerializeField] bool _isEnemy = false;

    public bool CanShoot { get; private set; }
    private bool _isShooting;
    private Vector3 _projectileDirectionVector;
    private Quaternion _projectileDirectionQuaternion;
    private string _projectileLayerName;

    private AudioSource _myAudioSource;
    private WeaponDataManager _weaponDataManager;
    private WeaponAmmoConsumptionManager _weaponAmmoConsumptionManager;

    private void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _projectileLayerName = _isEnemy ? ENEMY_PROJECTILE_LAYER : PLAYER_PROJECTILE_LAYER;
    }

    private void OnEnable()
    {
        CanShoot = true;
        if (_isEnemy)
        {
            _isShooting = true;
        }
    }

    private void Start()
    {
        _weaponDataManager = FindObjectsOfType<WeaponDataManager>()
            .First(manager => manager.ProjectileType == _projectileType && manager.IsEnemy == _isEnemy);
        _weaponAmmoConsumptionManager = FindObjectsOfType<WeaponAmmoConsumptionManager>()
            .First(manager => manager.ProjectileType == _projectileType);
    }

    private void Update() => HandleShooting();

    private void HandleShooting()
    {
        if (CanShoot && _isShooting && (_isEnemy || (!_isEnemy && _weaponAmmoConsumptionManager.CanShoot())))
        {
            StartCoroutine(ShotingCoroutine());
        }
    }

    private IEnumerator ShotingCoroutine()
    {
        ProjectileObjectPoolInstance.InstantiateProjectile(_projectileType, _projectileSpawnPosition,
            _projectileDirectionQuaternion != null ? _projectileDirectionQuaternion : Quaternion.identity,
            _projectileDirectionVector, _projectileLayerName);

        _weaponAmmoConsumptionManager.ConsumeAmmunition();

        CanShoot = false;

        PlayShootingSound();

        yield return new WaitForSeconds(_weaponDataManager.CurrentFiringSpeed);

        CanShoot = true;
    }

    private void PlayShootingSound()
    {
        _myAudioSource.pitch = Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();
    }

    public void IsShooting(bool isShooting) => _isShooting = isShooting;

    public void SetProjectileDirection(Vector3 projectileDirection) => _projectileDirectionVector = projectileDirection;

    public void SetProjectileDirection(Quaternion projectileDirection) => _projectileDirectionQuaternion = projectileDirection;
}
