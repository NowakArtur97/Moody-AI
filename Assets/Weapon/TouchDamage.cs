using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 10.0f;

    private void OnTriggerEnter2D(Collider2D collision) => collision.GetComponentInChildren<IDamagable>()?.DealDamage(_damageAmount);
}
