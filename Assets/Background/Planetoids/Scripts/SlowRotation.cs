using UnityEngine;

public class SlowRotation : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = 3.0f;
    [SerializeField] private float _maxRotationSpeed = 10.0f;

    private HealthSystem _healthSystem;
    private float _rotationAngle;
    private float _randomDirection;
    private float _rotationSpeed;

    private void Awake()
    {
        _healthSystem = GetComponentInChildren<HealthSystem>();
        _randomDirection = Random.Range(0, 2) * 2 - 1; // -1 or 1
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
    }

    private void Update()
    {
        if (_healthSystem == null || !_healthSystem.IsDying)
        {
            _rotationAngle += Time.deltaTime * _rotationSpeed * _randomDirection;
            transform.rotation = Quaternion.Euler(0, 0, _rotationAngle);
        }
    }
}
