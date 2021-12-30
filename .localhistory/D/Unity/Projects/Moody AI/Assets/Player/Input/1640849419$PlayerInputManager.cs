using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public void Movement(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }
}
