using UnityEngine;

public class TargetTransformPositionRotationHandler : MonoBehaviour
{
    [SerializeField] private EnemyTargetTransformHandler _enemyTargetTransformHandler;

    private Transform _targetTransform;

    private RotationController _rotationController;

    private void Awake()
    {
        _rotationController = GetComponentInChildren<RotationController>();
        _enemyTargetTransformHandler = _enemyTargetTransformHandler ?? GetComponent<EnemyTargetTransformHandler>();
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            _targetTransform = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }
        else
        {
            HandleRotation();
        }
    }

    private void HandleRotation() => _rotationController.SetTarget(_targetTransform.position);
}
