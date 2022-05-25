using static AmmoRestorationManager;

public class PlanetHealthSystem : HealthSystem
{
    public override void DeathTrigger()
    {
        // TODO: Handle destroying planet
    }

    public override void DealDamage(float damageAmount)
    {
        base.DealDamage(damageAmount);

        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DAMAGING_PLANET);
    }
}
