using UnityEngine;
using static CameraShake;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private SpaceMovementController _spaceMovementController;
    private WeaponSystem _weaponSystem;
    private bool _mouseInput;

    private void Start()
    {
        _spaceMovementController = GetComponent<SpaceMovementController>();
        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
        _weaponSystem = transform.parent.GetComponentInChildren<WeaponSystem>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
    }

    private void HandleMovementInput() => _spaceMovementController.SetMovementVector(_playerInputManager.MovementInput);

    private void HandleShootingInput()
    {
        _mouseInput = _playerInputManager.MouseInput;
        _weaponSystem.CurentWeapon.IsShooting(_mouseInput);

        if (_mouseInput && _weaponSystem.CurentWeapon.CanShoot)
        {
            CameraShakeInstance.Shake();
        }
    }
}
