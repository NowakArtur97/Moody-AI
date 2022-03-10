using UnityEngine;

public class ZigZagMovementTowardsTarget : MonoBehaviour
{
    [SerializeField] private float _speedOfSinMovement = 3.0f;
    [SerializeField] private float _sizeOfSinMovement = 1.0f;
    [SerializeField] private float _cycleSpeed = 3.0f;

    private Transform _targetTransform;
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;
    private Vector3 _position;
    private Vector3 _axis;

    private void Awake()
    {
        _position = transform.position;
        _axis = transform.right;
        _enemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            _targetTransform = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }

        _position += (_targetTransform.position - transform.position).normalized * Time.deltaTime * _cycleSpeed;
        transform.position = _position + _axis * Mathf.Sin(Time.time * _speedOfSinMovement) * _sizeOfSinMovement;
    }
}
