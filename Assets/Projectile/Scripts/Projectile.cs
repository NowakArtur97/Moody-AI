using UnityEngine;
using static ProjectileObjectPool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 20.0f;
    [SerializeField] private float _damage = 10.0f;
    [SerializeField] private ProjectileType _projectileType;

    private Vector3 _projectileDirection;

    private void Update() => transform.position += _projectileDirection * Time.deltaTime * _movementVelocity;

    public void SetDirection(Vector3 projectileDirection)
    {
        _projectileDirection = projectileDirection;
        _projectileDirection = new Vector3(_projectileDirection.x, _projectileDirection.y, 0.0f);
        float angleCorrection = Mathf.Atan2(_projectileDirection.y, _projectileDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleCorrection, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInChildren<IDamagable>()?.DealDamage(_damage);

        ProjectileObjectPoolInstance.ReleaseProjectile(gameObject, _projectileType);
    }
}
