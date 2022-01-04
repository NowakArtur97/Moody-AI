using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _projectileSpawnPosition;
    [SerializeField] float _offsetBetweenBullets = 0.2f;

    private bool _canShoot;
    private bool _isShooting;
    private Vector3 _projectileDirection;
    private GameObject _projectile;

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
        _projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity);
        _projectile.GetComponent<Projectile>().SetDirection(_projectileDirection);

        _canShoot = false;

        yield return new WaitForSeconds(_offsetBetweenBullets);

        _canShoot = true;
    }

    public void IsShooting(bool isShooting) => _isShooting = isShooting;

    public void SetProjectileDirection(Vector3 projectileDirection) => _projectileDirection = projectileDirection;
}
