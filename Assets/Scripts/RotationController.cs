using UnityEngine;

public class RotationController : MonoBehaviour
{
    private Vector3 _direction;
    private float _angle;

    [SerializeField] private Vector3 _position;

    private void Update()
    {
        _direction = transform.position - _position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }

    public void SetTarget(Vector2 position) => _position = position;
}
