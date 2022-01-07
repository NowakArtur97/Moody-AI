using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private float _helath;

    public void DealDamage(float damageAmount)
    {
        _helath -= damageAmount;

        if (_helath <= 0)
        {
            // TODO: HeathSystem: Return to pool
            Destroy(transform.parent.gameObject);
        }
    }
}
