using UnityEngine;

public class RotationController : MonoBehaviour
{
    private Vector3 _direction;
    private Vector3 _rotationVector;
    private float _angle;
    private Quaternion _targetRotation;
    private Vector3 _position;

    [SerializeField] private float _rotationSpeed = 250.0f;

    private void Update() => RotateTowardsPosition();

    private void RotateTowardsPosition()
    {
        _direction = _position - transform.position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _rotationVector.Set(0, 0, _angle);
        _targetRotation = Quaternion.Euler(_rotationVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Vector3 position) => _position = position;
}
