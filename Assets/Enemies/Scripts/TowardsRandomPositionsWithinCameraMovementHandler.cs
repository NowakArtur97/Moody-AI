using UnityEngine;

public class TowardsRandomPositionsWithinCameraMovementHandler : MonoBehaviour
{
    [SerializeField] private float _distanceWhenShouldFindNewPosition = 3.0f;

    private Vector3 _randomPosition;
    private int _screenWidth;
    private int _screenHeight;

    private Camera _mainCamera;
    private RotationController _rotationController;
    private SpaceMovementController _spaceMovementController;

    private void Start()
    {
        _mainCamera = Camera.main;
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;

        _rotationController = GetComponentInParent<RotationController>();
        _spaceMovementController = GetComponent<SpaceMovementController>();

        FindRandomPositionInCameraBounds();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _randomPosition) <= _distanceWhenShouldFindNewPosition)
        {
            FindRandomPositionInCameraBounds();
        }
    }

    private void FindRandomPositionInCameraBounds()
    {
        _randomPosition.Set(Random.Range(0, _screenWidth), Random.Range(0, _screenHeight), _mainCamera.farClipPlane / 2);
        _randomPosition = _mainCamera.ScreenToWorldPoint(_randomPosition);

        HandleMovementVector();
        HandleRotation();
    }

    private void HandleMovementVector() => _spaceMovementController.SetMovementVector((_randomPosition - transform.position).normalized);

    private void HandleRotation() => _rotationController.SetTarget(_randomPosition);
}
