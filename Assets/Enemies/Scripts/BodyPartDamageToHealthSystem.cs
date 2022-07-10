using UnityEngine;

public class BodyPartDamageToHealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyHealthSystem _enemyHealthSystem;

    public void DealDamage(float damageAmount) => _enemyHealthSystem.DealDamage(damageAmount);
}
