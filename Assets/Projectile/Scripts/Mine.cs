using UnityEngine;

public class Mine : BaseProjectile
{
    private Rigidbody2D _myRigidbody2D;

    protected override void Awake()
    {
        base.Awake();

        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _myRigidbody2D.AddForce(ProjectileDirection * MovementVelocity, ForceMode2D.Impulse);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        _myRigidbody2D.velocity = Vector2.zero;

        base.OnTriggerEnter2D(collision);
    }
}
