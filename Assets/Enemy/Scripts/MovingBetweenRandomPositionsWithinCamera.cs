using UnityEngine;

public class MovingBetweenRandomPositionsWithinCamera : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 10.0f;
    [SerializeField] private float _distanceWhenShouldFindNewPosition = 3.0f;

    private Vector3 _randomPosition;

    private Camera _mainCamera;
    private RotationController _rotationController;

    private void Start()
    {
        _mainCamera = Camera.main;

        _rotationController = GetComponentInChildren<RotationController>();

        FindRandomPositionInCameraBounds();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _randomPosition) > _distanceWhenShouldFindNewPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _randomPosition, _movementVelocity * Time.deltaTime);
        }
        else
        {
            FindRandomPositionInCameraBounds();
        }
    }

    private void FindRandomPositionInCameraBounds()
    {
        _randomPosition.Set(Random.Range(0, Screen.width), Random.Range(0, Screen.height), _mainCamera.farClipPlane / 2);
        _randomPosition = _mainCamera.ScreenToWorldPoint(_randomPosition);

        _rotationController.SetTarget(_randomPosition);
    }
}
