using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private SpaceMovementController _spaceMovementController;
    private Weapon _weapon;

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

    private void HandleShootingInput() => _weapon.IsShooting(_playerInputManager.MouseInput);
}
