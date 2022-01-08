using UnityEngine;

public class TargetTransformPositionRotationHandler : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private RotationController _rotationController;

    private void Start() => _rotationController = GetComponentInChildren<RotationController>();

    private void Update() => HandleRotation();

    private void HandleRotation() => _rotationController.SetTarget(_targetTransform.position);
}
