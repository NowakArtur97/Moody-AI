using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 10.0f;

    private IDamagable _healthSystem;

    private void Awake() => _healthSystem = GetComponentInChildren<IDamagable>();

    private void OnTriggerEnter2D(Collider2D collision) => _healthSystem?.DealDamage(_damageAmount);
}
