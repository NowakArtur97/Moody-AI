using UnityEngine;

public class TransformPositionRotationHandler : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private RotationController _rotationController;

    private void Start() => _rotationController = GetComponentInChildren<RotationController>();

    private void Update() => _rotationController.SetTarget(_targetTransform.position);
}
