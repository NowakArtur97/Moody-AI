using UnityEngine;
using static EnemyObjectPool;
using static AmmoRestorationManager;
using Pathfinding;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] private EnemyType _enemyType;

    public override void DeathTrigger()
    {
        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DEFEATING_ENEMIES);

        EnemyObjectPoolInstance.ReleaseEnemy(transform.parent.gameObject, _enemyType);
    }
}
