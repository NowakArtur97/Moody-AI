using UnityEngine;

public class TargetToWeaponHandler : MonoBehaviour
{
    private Transform _targetTransform;

    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        _weapon.IsShooting(true);
        _targetTransform = GetComponentInParent<EnemyTargetTransformHandler>().TargetTransform;
    }

    private void Update() => HandleShootingDirection();

    private void HandleShootingDirection() => _weapon.SetProjectileDirection((_targetTransform.position - transform.position).normalized);
}
