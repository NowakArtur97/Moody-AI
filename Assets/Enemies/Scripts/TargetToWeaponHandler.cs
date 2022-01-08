using UnityEngine;

public class TargetToWeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        _weapon.IsShooting(true);
    }

    private void Update() => HandleShootingDirection();

    private void HandleShootingDirection() => _weapon.SetProjectileDirection((_targetTransform.position - transform.position).normalized);
}
