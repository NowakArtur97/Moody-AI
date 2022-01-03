using UnityEngine;

public class MovingBetweenRandomPositionsWithinCamera : MonoBehaviour
{
    [SerializeField] private float _distanceWhenShouldFindNewPosition = 3.0f;

    private Vector3 _randomPosition;

    private Camera _mainCamera;
    private RotationController _rotationController;
    private SpaceMovementController _spaceMovementController;

    private void Start()
    {
        _mainCamera = Camera.main;

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
        HandleMovementVector();
    }

    private void FindRandomPositionInCameraBounds()
    {
        _randomPosition.Set(Random.Range(0, Screen.width), Random.Range(0, Screen.height), _mainCamera.farClipPlane / 2);
        _randomPosition = _mainCamera.ScreenToWorldPoint(_randomPosition);

        _rotationController.SetTarget(_randomPosition);
    }

    private void HandleMovementVector() => _spaceMovementController.SetMovementVector((_randomPosition - transform.position).normalized);
}
