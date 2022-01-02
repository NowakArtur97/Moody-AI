using UnityEngine;

public class GettingCloserAndRotatingAroundTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _movementVelocity = 10.0f;
    [SerializeField] private float _rotatingVelocity = 10.0f;
    [SerializeField] private float _distanceWhenShouldStop = 3.0f;

    private Vector3 _zAxis;

    private void Awake() => _zAxis = new Vector3(0, 0, 1);

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, _target.position) > _distanceWhenShouldStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _movementVelocity * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(_target.position, _zAxis, _rotatingVelocity * Time.deltaTime);
        }
    }
}
