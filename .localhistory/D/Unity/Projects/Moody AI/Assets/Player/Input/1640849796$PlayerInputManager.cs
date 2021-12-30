using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 Movement { get; private set; }

    public void Movement(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }
}
