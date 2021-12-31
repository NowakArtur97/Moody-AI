using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private SpaceshipController _spaceshipController;
    private PlayerInputManager _playerInputManager;

    private void Start()
    {
        _spaceshipController = GetComponent<SpaceshipController>();
        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
    }

    private void Update() => HandleMovementInput();

    private void HandleMovementInput() => _spaceshipController.SetMovementInput(_playerInputManager.MovementInput);
}
