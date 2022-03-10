using UnityEngine;

public class TowardsTargetSpaceMovementHandler : MonoBehaviour
{
    private Transform _targetTransform;

    private SpaceMovementController _spaceMovementController;
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;

    private void Awake()
    {
        _spaceMovementController = GetComponent<SpaceMovementController>();
        _enemyTargetTransformHandler = GetComponentInParent<EnemyTargetTransformHandler>();
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            _targetTransform = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }

        HandleMovementVector();
    }

    private void HandleMovementVector() => _spaceMovementController.SetMovementVector((_targetTransform.position - transform.position).normalized);
}
