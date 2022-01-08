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

    private bool _canShoot;
    private bool _isShooting;
    private Vector3 _projectileDirectionVector;
    private Quaternion _projectileDirectionQuaternion;
    private ProjectileType _projectileType;
    private string _projectileLayerName;

    private AudioSource _myAudioSource;

    private void Awake()
    {
        _canShoot = true;
        _myAudioSource = GetComponent<AudioSource>();
    }

    private void Update() => HandleShooting();

    private void HandleShooting()
    {
        if (_canShoot && _isShooting)
        {
            StartCoroutine(ShotingCoroutine());
        }
    }

    private IEnumerator ShotingCoroutine()
    {
        ProjectileObjectPoolInstance.InstantiateProjectile(_projectileType, _projectileSpawnPosition,
            _projectileDirectionQuaternion != null ? _projectileDirectionQuaternion : Quaternion.identity, _projectileDirectionVector,
            _projectileLayerName);

        _canShoot = false;

        _myAudioSource.pitch = Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();

        yield return new WaitForSeconds(_offsetBetweenBullets);

        _canShoot = true;
    }

    public void IsShooting(bool isShooting) => _isShooting = isShooting;

    public void SetProjectileDirection(Vector3 projectileDirection) => _projectileDirectionVector = projectileDirection;

    public void SetProjectileDirection(Quaternion projectileDirection) => _projectileDirectionQuaternion = projectileDirection;

    public void SetProjectileType(ProjectileType projectileType) => _projectileType = projectileType;

    public void SetProjectileLayerName(string projectileLayerName) => _projectileLayerName = projectileLayerName;
}
