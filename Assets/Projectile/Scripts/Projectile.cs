using UnityEngine;

public class Projectile : BaseProjectile
{
    private void Update()
    {
        if (IsMoving)
        {
            transform.position += ProjectileDirection * Time.deltaTime * WeaponDataManager.CurrentMovementSpeed;
        }
    }
}
