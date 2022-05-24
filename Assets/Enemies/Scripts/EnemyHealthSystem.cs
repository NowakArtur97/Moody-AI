using System.Collections;
using UnityEngine;
using static EnemyObjectPool;
using static AmmoRestorationManager;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] private EnemyType _enemyType;

    protected override IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(MyAudioSource.clip.length);

        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DEFEATING_ENEMIES);

        EnemyObjectPoolInstance.ReleaseEnemy(transform.parent.gameObject, _enemyType);
    }
}
