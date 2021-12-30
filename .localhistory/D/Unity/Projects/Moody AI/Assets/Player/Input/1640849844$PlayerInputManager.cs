using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }

    public void Movement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>().normalized;
        Debug.Log(MovementInput);
    }
}
