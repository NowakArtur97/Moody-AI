using UnityEngine;

public class ZigZagMovementTowardsTarget : MonoBehaviour
{
    [SerializeField] private float _speedOfSinMovement = 3.0f;
    [SerializeField] private float _sizeOfSinMovement = 1.0f;
    [SerializeField] private float _cycleSpeed = 3.0f;

    private Transform _targetTransform;
    private Vector3 _position;
    private Vector3 _axis;

    private void Start()
    {
        _position = transform.position;
        _axis = transform.right;
        _targetTransform = GetComponent<EnemyTargetTransformHandler>().TargetTransform;
    }

    private void Update()
    {
        _position += (_targetTransform.position - transform.position).normalized * Time.deltaTime * _cycleSpeed;
        transform.position = _position + _axis * Mathf.Sin(Time.time * _speedOfSinMovement) * _sizeOfSinMovement;
    }
}
