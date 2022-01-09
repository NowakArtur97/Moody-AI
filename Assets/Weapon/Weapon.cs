using System.Collections;
using UnityEngine;
using static ProjectileObjectPool;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _projectileSpawnPosition;
    [SerializeField] float _offsetBetweenBullets = 0.2f;
    [SerializeField] float _minSoundPitch = 0.95f;
    [SerializeField] float _maxSoundPitch = 1.05f;

    public bool CanShoot { get; private set; }
    private bool _isShooting;
    private Vector3 _projectileDirectionVector;
    private Quaternion _projectileDirectionQuaternion;
    private ProjectileType _projectileType;
    private string _projectileLayerName;

    private AudioSource _myAudioSource;

    private void Awake()
    {
        CanShoot = true;
        _myAudioSource = GetComponent<AudioSource>();
    }

    private void Update() => HandleShooting();

    private void HandleShooting()
    {
        if (CanShoot && _isShooting)
        {
            StartCoroutine(ShotingCoroutine());
        }
    }

    private IEnumerator ShotingCoroutine()
    {
        ProjectileObjectPoolInstance.InstantiateProjectile(_projectileType, _projectileSpawnPosition,
            _projectileDirectionQuaternion != null ? _projectileDirectionQuaternion : Quaternion.identity, _projectileDirectionVector,
            _projectileLayerName);

        CanShoot = false;

        PlayShootingSound();

        yield return new WaitForSeconds(_offsetBetweenBullets);

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

    public void SetProjectileType(ProjectileType projectileType) => _projectileType = projectileType;

    public void SetProjectileLayerName(string projectileLayerName) => _projectileLayerName = projectileLayerName;
}
