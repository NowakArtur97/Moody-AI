using UnityEngine;

public class RotationController : MonoBehaviour
{
    private Vector3 _rotationVector;
    private float _angle;
    private Quaternion _targetRotation;
    private Vector3 _position;

    [SerializeField] private float _rotationSpeed = 250.0f;
    [SerializeField] private RotationMode _rotationMode = RotationMode.TOWARDS;
    public RotationMode Rotationmode
    {
        get { return _rotationMode; }
        set { _rotationMode = value; }
    }

    public enum RotationMode { TOWARDS, PERPEDICULAR }

    private void Update()
    {
        switch (_rotationMode)
        {
            case RotationMode.TOWARDS:
                RotateTowardsPosition();
                break;
            case RotationMode.PERPEDICULAR:
                RotatePerpendicularToPosition();
                break;
        }
    }

    private void RotateTowardsPosition() => RotateToPosition(_position - transform.position);

    private void RotatePerpendicularToPosition() => RotateToPosition(Vector2.Perpendicular(_position - transform.position));

    private void RotateToPosition(Vector2 direction)
    {
        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rotationVector.Set(0, 0, _angle);
        _targetRotation = Quaternion.Euler(_rotationVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Vector3 position) => _position = position;

    public void SetRotationMode(RotationMode rotationMode) => _rotationMode = rotationMode;
}
