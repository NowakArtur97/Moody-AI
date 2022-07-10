using UnityEngine;

public class SpecificTransformPositionRotationHandler : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private RotationController _rotationController;

    private void Awake() => _rotationController = GetComponentInChildren<RotationController>();

    private void Update() => HandleRotation();

    private void HandleRotation() => _rotationController.SetTarget(_targetTransform.position);
}