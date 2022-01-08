using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 5.0f;
    [SerializeField] float _offsetBetweenDealingDamage = 0.5f;

    private bool _canAttack;
    private List<IDamagable> _toDamage;

    private void Awake()
    {
        _canAttack = true;
        _toDamage = new List<IDamagable>();
    }

    private void Update() => DealDamage();

    private void DealDamage()
    {
        if (_canAttack && _toDamage.Count > 0)
        {
            StartCoroutine(DamageCoroutine());
        }
    }

    private IEnumerator DamageCoroutine()
    {
        _canAttack = false;

        // TODO: DamageDealer: Attack sounds
        _toDamage.ForEach(damagable => damagable.DealDamage(_damageAmount));

        yield return new WaitForSeconds(_offsetBetweenDealingDamage);

        _canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable toDamage = collision.GetComponentInChildren<IDamagable>();

        if (toDamage != null)
        {
            _toDamage.Add(toDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamagable toDamage = collision.GetComponentInChildren<IDamagable>();

        if (toDamage != null)
        {
            _toDamage.Remove(toDamage);
        }
    }
}
