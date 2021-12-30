using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 Movement { get; private set; }

    public void Movement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>().normalized;
    }
}
