using System;
using UnityEngine;
using static AmmoRestorationManager;

public class PlanetHealthSystem : HealthSystem
{
    private readonly string SHIELD_WRAPPER_GAME_OBJECT_NAME = "Shield Wrapper(Clone)";

    public Action<Transform> OnPlanetDestroyed;

    public override void DeathTrigger() => OnPlanetDestroyed?.Invoke(transform.parent);

    public override void DealDamage(float damageAmount)
    {
        base.DealDamage(damageAmount);

        if (IsDying)
        {
            DestroyShield();
        }

        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DAMAGING_PLANET);
    }

    private void DestroyShield()
    {
        Transform shieldWrapper = transform.parent.Find(SHIELD_WRAPPER_GAME_OBJECT_NAME);

        if (shieldWrapper)
        {
            Destroy(shieldWrapper.gameObject);
        }
    }
}
