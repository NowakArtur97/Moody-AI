using System.Collections;
using UnityEngine;
using static AmmoRestorationManager;

public class PlanetHealthSystem : HealthSystem
{
    protected override IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(MyAudioSource.clip.length);

        // TODO: Handle destroying planet
    }

    public override void DealDamage(float damageAmount)
    {
        base.DealDamage(damageAmount);

        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DAMAGING_PLANET);
    }
}
