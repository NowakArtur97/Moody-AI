using UnityEngine;

public class MousePositionRotationHandler : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private RotationController _rotationController;
    private WeaponSystem _weaponSystem;

    private void Start()
    {
        _playerInputManager = GetComponentInChildren<PlayerInputManager>();
        _rotationController = GetComponentInChildren<RotationController>();
        _weaponSystem = GetComponentInChildren<WeaponSystem>();
    }

    private void Update()
    {
        HandleRotation();
        HandleProjectileDirection();
    }

    private void HandleRotation() => _rotationController.SetTarget(_playerInputManager.MouseWorldPosition);

    private void HandleProjectileDirection()
    {
        if (_weaponSystem.CurentWeapon)
        {
            _weaponSystem.CurentWeapon.SetProjectileDirection(transform.rotation);
            _weaponSystem.CurentWeapon.SetProjectileDirection(transform.right);
        }
    }
}
