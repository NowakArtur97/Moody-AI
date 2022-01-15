using UnityEngine;

public class TowardsTargetAndRandomPositionsWithinCameraMovementHandler : MonoBehaviour
{
    [SerializeField] private float _distanceWhenShouldFindChangePosition = 2.0f;
    [SerializeField] private float _randomPositionAccelerationFactor = 300.0f;
    [SerializeField] private float _targetPositionAccelerationFactor = 30.0f;

    private Transform _targetTransform;
    private Vector3 _targetPosition;
    private bool _shouldPickRandomPosition;
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

        _targetTransform = GetComponentInParent<EnemyTargetTransformHandler>().TargetTransform;

        AimForTarget();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _targetPosition) <= _distanceWhenShouldFindChangePosition)
        {
            if (_shouldPickRandomPosition)
            {
                FindRandomPositionInCameraBounds();
            }
            else
            {
                AimForTarget();
            }
        }

        HandleRotation();
        HandleMovementVector();
    }

    private void FindRandomPositionInCameraBounds()
    {
        _targetPosition.Set(Random.Range(0, _screenWidth), Random.Range(0, _screenHeight), _mainCamera.farClipPlane / 2);
        _targetPosition = _mainCamera.ScreenToWorldPoint(_targetPosition);

        _spaceMovementController.SetAccelerationFactor(_randomPositionAccelerationFactor);

        _shouldPickRandomPosition = false;
    }

    private void AimForTarget()
    {
        _targetPosition = _targetTransform.position;
        _spaceMovementController.SetAccelerationFactor(_targetPositionAccelerationFactor);
        _shouldPickRandomPosition = true;
    }

    private void HandleMovementVector() => _spaceMovementController.SetMovementVector((_targetPosition - transform.position).normalized);

    private void HandleRotation() => _rotationController.SetTarget(_targetPosition);
}
