public class PlanetoidHealthSystem : HealthSystem
{
    public override void DeathTrigger() => Destroy(transform.parent.gameObject);
}
