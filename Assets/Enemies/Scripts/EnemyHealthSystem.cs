using UnityEngine;
using static EnemyObjectPool;
using static AmmoRestorationManager;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private GameObject _gameObjectToRelease;

    protected override void Awake()
    {
        base.Awake();

        if (_gameObjectToRelease == null)
        {
            _gameObjectToRelease = transform.parent.gameObject;
        }
    }

    public override void DeathTrigger()
    {
        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DEFEATING_ENEMIES);

        EnemyObjectPoolInstance.ReleaseEnemy(_gameObjectToRelease, _enemyType);
    }
}
