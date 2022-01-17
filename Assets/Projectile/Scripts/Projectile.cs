using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Projectile : BaseProjectile
{
    private Vector3 _projectileDirection;

    private void Update() => transform.position += _projectileDirection * Time.deltaTime * MovementVelocity;

    public void SetDirection(Vector3 projectileDirection)
    {
        _projectileDirection = projectileDirection;
        _projectileDirection = new Vector3(_projectileDirection.x, _projectileDirection.y, 0.0f);
        float angleCorrection = Mathf.Atan2(_projectileDirection.y, _projectileDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleCorrection, Vector3.forward);
    }
}
