using UnityEngine;

public class TargetTransformPositionRotationHandler : MonoBehaviour
{
    [SerializeField] EnemyTargetTransformHandler _enemyTargetTransformHandler;

    private Transform _targetTransform;

    private RotationController _rotationController;

    private void Awake() => _rotationController = GetComponentInChildren<RotationController>();

    private void Start() => _targetTransform = (_enemyTargetTransformHandler ?? GetComponent<EnemyTargetTransformHandler>()).TargetTransform;

    private void Update() => HandleRotation();

    private void HandleRotation() => _rotationController.SetTarget(_targetTransform.position);
}
