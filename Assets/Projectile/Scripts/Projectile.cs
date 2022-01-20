using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Projectile : BaseProjectile
{
    private void Update()
    {
        if (IsMoving)
        {
            transform.position += ProjectileDirection * Time.deltaTime * MovementVelocity;
        }
    }
}
