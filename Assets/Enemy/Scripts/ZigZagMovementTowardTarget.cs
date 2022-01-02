using UnityEngine;

public class ZigZagMovementTowardTarget : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _speedOfSinMovement = 3.0f;
    [SerializeField] private float _sizeOfSinMovement = 1.0f;
    [SerializeField] private float _cycleSpeed = 3.0f;

    private Vector3 _position;
    private Vector3 _axis;

    private void Start()
    {
        _position = transform.position;
        _axis = transform.right;
    }

    private void Update()
    {
        _position += (_targetTransform.position - transform.position).normalized * Time.deltaTime * _cycleSpeed;
        transform.position = _position + _axis * Mathf.Sin(Time.time * _speedOfSinMovement) * _sizeOfSinMovement;
    }
}
