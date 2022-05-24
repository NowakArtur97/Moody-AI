using System;
using System.Collections;
using UnityEngine;
using static AmmoRestorationManager;

public class PlayerHealthSystem : HealthSystem
{
    public Action OnPlayerDeath;

    protected override IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(MyAudioSource.clip.length);

        OnPlayerDeath?.Invoke();
    }

    public override void DealDamage(float damageAmount)
    {
        base.DealDamage(damageAmount);

        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.TAKING_DAMAGE);
    }
}
