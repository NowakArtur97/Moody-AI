using UnityEngine;
using static CameraShake;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private SpaceMovementController _spaceMovementController;
    private Weapon _weapon;
    private bool _mouseInput;

    private void Start()
    {
        _spaceMovementController = GetComponent<SpaceMovementController>();
        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
        _weapon = transform.parent.GetComponentInChildren<Weapon>();
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
        _weapon.IsShooting(_mouseInput);

        if (_mouseInput && _weapon.CanShoot)
        {
            CameraShakeInstance.Shake();
        }
    }
}
