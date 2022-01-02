using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private SpaceMovementController _spaceMovementController;
    private PlayerInputManager _playerInputManager;

    private void Start()
    {
        _spaceMovementController = GetComponent<SpaceMovementController>();
        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
    }

    private void Update() => HandleMovementInput();

    private void HandleMovementInput() => _spaceMovementController.SetMovementVector(_playerInputManager.MovementInput);
}
