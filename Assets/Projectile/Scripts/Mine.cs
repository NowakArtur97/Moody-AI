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

        _myRigidbody2D.AddForce(transform.right * MovementVelocity, ForceMode2D.Impulse);
    }
}
