using UnityEngine;

public class MousePositionRotationHandler : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private RotationController _rotationController;
    private Weapon _weapon;

    private void Start()
    {
        _playerInputManager = GetComponentInChildren<PlayerInputManager>();
        _rotationController = GetComponentInChildren<RotationController>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        HandleRotation();
        HandleProjectileDirection();
    }

    private void HandleRotation() => _rotationController.SetTarget(_playerInputManager.MouseWorldPosition);

    private void HandleProjectileDirection() => _weapon.SetProjectileDirection((_playerInputManager.MouseWorldPosition - transform.position).normalized);
}
