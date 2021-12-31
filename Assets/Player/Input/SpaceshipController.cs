using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] private float _accelerationFactor = 30.0f;

    private Vector2 _movementInput;
    private Vector2 _movementForceVector;

    private Rigidbody2D _myRigidbody2D;

    private void Awake() => _myRigidbody2D = GetComponentInParent<Rigidbody2D>();

    private void FixedUpdate() => ApplyMovementForces();

    private void ApplyMovementForces()
    {
        _movementForceVector = Vector2.one * _accelerationFactor * _movementInput;
        _myRigidbody2D.AddForce(_movementForceVector, ForceMode2D.Force);
    }

    public void SetMovementInput(Vector2 movementInput) => _movementInput = movementInput;
}
