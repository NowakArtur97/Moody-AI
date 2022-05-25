using System;
using static AmmoRestorationManager;

public class PlayerHealthSystem : HealthSystem
{
    public Action OnPlayerDeath;

    public override void DeathTrigger() => OnPlayerDeath?.Invoke();

    public override void DealDamage(float damageAmount)
    {
        base.DealDamage(damageAmount);

        AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.TAKING_DAMAGE);
    }
}
