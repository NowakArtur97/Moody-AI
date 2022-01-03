using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _projectileSpawnPosition;
    [SerializeField] float _offsetBetweenBullets = 0.2f;

    private bool _canShoot;
    private bool _isShooting;
    private Quaternion _projectileRotation;

    private void Awake() => _canShoot = true;

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
        Instantiate(_projectile, _projectileSpawnPosition.position, _projectileRotation);

        _canShoot = false;

        yield return new WaitForSeconds(_offsetBetweenBullets);

        _canShoot = true;
    }

    public void IsShooting(bool isShooting) => _isShooting = isShooting;

    public void SetProjectileDirection(Vector3 directionVector) => _projectileRotation = Quaternion.LookRotation(directionVector, Vector3.up);
}
