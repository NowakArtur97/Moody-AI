using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    private const float SPRITE_ROTATION_OFFSET = 270f;

    private Vector3 _direction;
    private float _angle;

    private Camera _mainCamera;
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        _mainCamera = Camera.main;
        playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
    }

    private void Update()
    {
        _direction = transform.position - playerInputManager.MouseWorldPosition;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - SPRITE_ROTATION_OFFSET;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }
}
