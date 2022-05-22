using UnityEngine;

public class Shuriken : BaseProjectile
{
    [SerializeField] float _rotationSpeed = 10.0f;

    private void Update()
    {
        transform.position += ProjectileDirection * Time.deltaTime * WeaponDataManager.CurrentMovementSpeed;
        transform.Rotate(Vector3.forward * (_rotationSpeed * Time.deltaTime));
    }
}
