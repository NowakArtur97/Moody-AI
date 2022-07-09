using UnityEngine;
using static EnemyObjectPool;
using static AmmoRestorationManager;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private GameObject _gameObjectToRelease;

    public override void DeathTrigger()
    {
        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DEFEATING_ENEMIES);

        EnemyObjectPoolInstance.ReleaseEnemy(_gameObjectToRelease ?? transform.parent.gameObject, _enemyType);
    }
}
