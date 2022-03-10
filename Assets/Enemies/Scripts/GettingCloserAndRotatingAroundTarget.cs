using UnityEngine;

public class GettingCloserAndRotatingAroundTarget : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 10.0f;
    [SerializeField] private float _rotatingVelocity = 50.0f;
    [SerializeField] private float _distanceWhenShouldRotateAround = 8.0f;

    private Transform _targetTransform;
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;
    private Vector3 _zAxis;
    private bool _shouldRotateAround;

    private void Awake()
    {
        _zAxis = new Vector3(0, 0, 1);
        _enemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();
    }

    private void FixedUpdate()
    {
        if (_targetTransform == null)
        {
            _targetTransform = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }

        CheckIfDistanceReached();

        if (_shouldRotateAround)
        {
            transform.RotateAround(_targetTransform.position, _zAxis, -_rotatingVelocity * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, _movementVelocity * Time.deltaTime);
        }
    }

    private void CheckIfDistanceReached()
    {
        if (Vector2.Distance(transform.position, _targetTransform.position) <= _distanceWhenShouldRotateAround)
        {
            GetComponent<RotationController>().SetRotationMode(RotationController.RotationMode.PERPEDICULAR);
            _shouldRotateAround = true;
        }
    }
}
