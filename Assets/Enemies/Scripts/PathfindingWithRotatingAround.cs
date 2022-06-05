using Pathfinding;
using UnityEngine;

public class PathfindingWithRotatingAround : MonoBehaviour
{
    [SerializeField] private float _rotatingVelocity = 100.0f;

    private EnemyTargetTransformHandler _enemyTargetTransformHandler;
    private AIPath _aiPath;
    private RotationController _rotationController;

    private Transform _targetTransform;
    private Vector3 _zAxis;
    private bool _shouldRotateAround;

    private void Awake()
    {
        _zAxis = new Vector3(0, 0, 1);
        _enemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();
        _aiPath = GetComponent<AIPath>();
        _rotationController = GetComponent<RotationController>();
    }

    private void FixedUpdate()
    {
        if (_targetTransform == null)
        {
            FindTarget();
        }

        CheckIfDistanceReached();

        if (_shouldRotateAround)
        {
            RotateAround();
        }
    }

    private void FindTarget()
    {
        _shouldRotateAround = false;
        _targetTransform = _enemyTargetTransformHandler.FindRandomTargetTransform();
        _rotationController.SetRotationMode(RotationController.RotationMode.TOWARDS);
        _aiPath.canMove = true;
    }

    private void CheckIfDistanceReached()
    {
        if (!_shouldRotateAround && Vector2.Distance(transform.position, _targetTransform.position) <= _aiPath.endReachedDistance)
        {
            OnPathCompleted();
        }
    }

    private void OnPathCompleted()
    {
        _rotationController.SetRotationMode(RotationController.RotationMode.PERPEDICULAR);
        _shouldRotateAround = true;
        _aiPath.canMove = false;
    }

    private void RotateAround() => transform.RotateAround(_targetTransform.position, _zAxis, -_rotatingVelocity * Time.deltaTime);
}
