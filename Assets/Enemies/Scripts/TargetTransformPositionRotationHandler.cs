using UnityEngine;

public class TargetTransformPositionRotationHandler : MonoBehaviour
{
    private Transform _targetTransform;

    private RotationController _rotationController;

    private void Awake() => _rotationController = GetComponentInChildren<RotationController>();

    private void Start() => _targetTransform = GetComponent<EnemyTargetTransformHandler>().TargetTransform;

    private void Update() => HandleRotation();

    private void HandleRotation() => _rotationController.SetTarget(_targetTransform.position);
}
