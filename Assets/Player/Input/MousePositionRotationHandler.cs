using UnityEngine;

public class MousePositionRotationHandler : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private RotationController _rotationController;

    private void Start()
    {
        _playerInputManager = GetComponentInChildren<PlayerInputManager>();
        _rotationController = GetComponentInChildren<RotationController>();
    }

    private void Update() => _rotationController.SetTarget(_playerInputManager.MouseWorldPosition);
}
