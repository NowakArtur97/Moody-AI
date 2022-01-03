using UnityEngine;

public class RestrictPositionToViewport : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _positionLimit;

    private void Start() => _mainCamera = Camera.main;

    private void Update() => LimitPositionToCameraBounds();

    private void LimitPositionToCameraBounds()
    {
        _positionLimit = _mainCamera.WorldToViewportPoint(transform.position);
        _positionLimit.x = Mathf.Clamp01(_positionLimit.x);
        _positionLimit.y = Mathf.Clamp01(_positionLimit.y);
        transform.position = _mainCamera.ViewportToWorldPoint(_positionLimit);
    }
}
