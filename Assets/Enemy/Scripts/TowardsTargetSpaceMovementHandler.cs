using UnityEngine;

public class TowardsTargetSpaceMovementHandler : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private SpaceMovementController _spaceMovementController;

    private void Start() => _spaceMovementController = GetComponent<SpaceMovementController>();

    private void Update() => HandleMovementVector();

    private void HandleMovementVector() => _spaceMovementController.SetMovementVector((_targetTransform.position - transform.position).normalized);
}
