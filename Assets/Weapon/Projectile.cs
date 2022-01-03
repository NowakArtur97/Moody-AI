using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 20.0f;

    private Vector3 _projectileDirection;

    private void Update() => transform.position += _projectileDirection * Time.deltaTime * _movementVelocity;

    public void SetDirection(Vector3 projectileDirection)
    {
        _projectileDirection = projectileDirection;
        _projectileDirection = new Vector3(_projectileDirection.x, _projectileDirection.y, 0.0f);
    }
}
