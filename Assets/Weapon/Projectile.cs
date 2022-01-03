using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 20.0f;

    private Rigidbody2D _myRigidbody2D;

    private void Awake()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();

        _myRigidbody2D.velocity = transform.forward * _movementVelocity;
    }
}
