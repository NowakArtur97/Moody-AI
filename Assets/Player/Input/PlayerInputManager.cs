using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool MouseInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }

    private Camera _mainCamera;
    private Mouse _currentMouse;

    private void Start()
    {
        _mainCamera = Camera.main;
        _currentMouse = Mouse.current;
    }

    public void Movement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();

    public void Shooting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseInput = true;
        }
        if (context.canceled)
        {
            MouseInput = false;
        }
    }

    private void Update() => ReadMousePosition();

    private void ReadMousePosition() => MouseWorldPosition = _mainCamera.ScreenToWorldPoint(_currentMouse.position.ReadValue());
}
