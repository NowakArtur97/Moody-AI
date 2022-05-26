using UnityEngine;
using static AmmoRestorationManager;

public class PlanetHealthSystem : HealthSystem
{
    private readonly string SHIELD_WRAPPER_GAME_OBJECT_NAME = "Shield Wrapper(Clone)";

    public override void DeathTrigger()
    {
        // TODO: Reduce number of planets
        Debug.Log("Planet destroyed");
        Destroy(transform.parent.gameObject);
    }

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
