using UnityEngine;

public class TargetToWeaponHandler : MonoBehaviour
{
    private Transform _targetTransform;

    private Weapon _weapon;
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _enemyTargetTransformHandler = GetComponentInParent<EnemyTargetTransformHandler>();
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            _targetTransform = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }
        else
        {
            HandleShootingDirection();
        }
    }

    private void HandleShootingDirection() => _weapon.SetProjectileDirection((_targetTransform.position - transform.position).normalized);
}
