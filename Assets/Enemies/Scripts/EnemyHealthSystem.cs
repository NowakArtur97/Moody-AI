using UnityEngine;
using static EnemyObjectPool;
using static AmmoRestorationManager;
using System;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private GameObject _gameObjectToRelease;

    public Action OnEnemyDeath;

    protected override void Awake()
    {
        base.Awake();

        if (_gameObjectToRelease == null)
        {
            _gameObjectToRelease = transform.parent.gameObject;
        }
    }

    public override void DealDamage(float damageAmount)
    {
        base.DealDamage(damageAmount);

        if (IsDying)
        {
            OnEnemyDeath?.Invoke();
        }
    }

    public override void DeathTrigger()
    {
        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DEFEATING_ENEMIES);

        EnemyObjectPoolInstance.ReleaseEnemy(_gameObjectToRelease, _enemyType);
    }
}
