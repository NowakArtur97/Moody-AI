using UnityEngine;

public class TowardsTargetSpaceMovementHandler : MonoBehaviour
{
    private Transform _targetTransform;

    private SpaceMovementController _spaceMovementController;

    private void Start()
    {
        _spaceMovementController = GetComponent<SpaceMovementController>();
        _targetTransform = GetComponentInParent<EnemyTargetTransformHandler>().TargetTransform;
    }

    private void Update() => HandleMovementVector();

    private void HandleMovementVector() => _spaceMovementController.SetMovementVector((_targetTransform.position - transform.position).normalized);
}
