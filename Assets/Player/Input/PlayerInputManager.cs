using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }

    private Camera _mainCamera;
    private Vector2Control _currentMousePosition;

    private void Start()
    {
        _mainCamera = Camera.main;
        _currentMousePosition = Mouse.current.position;
    }

    public void Movement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();

    private void Update() => ReadMousePosition();

    private void ReadMousePosition() => MouseWorldPosition = _mainCamera.ScreenToWorldPoint(_currentMousePosition.ReadValue());
}
