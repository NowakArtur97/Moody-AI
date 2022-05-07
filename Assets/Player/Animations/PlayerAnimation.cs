using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private readonly string MOVING_RIGHT_ANIMATION_BOOL_NAME = "isMovingRight";
    private readonly string MOVING_LEFT_ANIMATION_BOOL_NAME = "isMovingLeft";

    private float _xInput;

    private Animator _myAnimator;
    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();

        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
    }

    private void Update()
    {
        _xInput = _playerInputManager.MovementInput.x;

        if (_xInput == 1)
        {
            SetAnimationBoolVariables(true, false);
        }
        else if (_xInput == -1)
        {
            SetAnimationBoolVariables(false, true);
        }
        else
        {
            SetAnimationBoolVariables(false, false);
        }
    }

    private void SetAnimationBoolVariables(bool isMovingRight, bool isMovingLeft)
    {
        _myAnimator.SetBool(MOVING_RIGHT_ANIMATION_BOOL_NAME, isMovingRight);
        _myAnimator.SetBool(MOVING_LEFT_ANIMATION_BOOL_NAME, isMovingLeft);
    }
}
