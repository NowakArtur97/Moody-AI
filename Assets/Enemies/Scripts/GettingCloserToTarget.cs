using UnityEngine;

public class GettingCloserToTarget : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 10.0f;
    [SerializeField] private float _distanceWhenShouldStop = 3.0f;

    private Transform _targetTransform;

    private void Start() => _targetTransform = GetComponent<EnemyTargetTransformHandler>().TargetTransform;

    private void Update()
    {
        if (Vector2.Distance(transform.position, _targetTransform.position) > _distanceWhenShouldStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, _movementVelocity * Time.deltaTime);
        }
    }
}
