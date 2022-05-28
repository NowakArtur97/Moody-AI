using UnityEngine;

public class GettingCloserToTarget : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 10.0f;
    [SerializeField] private float _distanceWhenShouldStop = 3.0f;

    private Transform _targetTransform;
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;

    private void Awake() => _enemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();

    private void Update()
    {
        if (_targetTransform == null)
        {
            _targetTransform = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }

        if (_targetTransform != null && Vector2.Distance(transform.position, _targetTransform.position) > _distanceWhenShouldStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, _movementVelocity * Time.deltaTime);
        }
    }
}
