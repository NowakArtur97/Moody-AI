using UnityEngine;

public class SpaceMovementController : MonoBehaviour
{
    [SerializeField] private float _accelerationFactor = 30.0f;
    [SerializeField] private float _maxVelocityMagnitude = 30.0f;

    private Vector2 _movementVector;
    private Vector2 _movementForceVector;

    private Rigidbody2D _myRigidbody2D;

    private void Awake() => _myRigidbody2D = GetComponentInParent<Rigidbody2D>();

    private void FixedUpdate() => ApplyMovementForces();

    private void ApplyMovementForces()
    {
        if (_maxVelocityMagnitude < _myRigidbody2D.velocity.magnitude)
        {
            return;
        }

        _movementForceVector = Vector2.one * _accelerationFactor * _movementVector;
        _myRigidbody2D.AddForce(_movementForceVector, ForceMode2D.Force);
    }

    public void SetMovementVector(Vector2 movementVector) => _movementVector = movementVector;

    public void SetAccelerationFactor(float accelerationFactor) => _accelerationFactor = accelerationFactor;
}
