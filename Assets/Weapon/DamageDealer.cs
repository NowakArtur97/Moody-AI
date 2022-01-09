using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 5.0f;
    [SerializeField] float _offsetBetweenDealingDamage = 0.5f;
    [SerializeField] float _minSoundPitch = 0.95f;
    [SerializeField] float _maxSoundPitch = 1.05f;

    private bool _canAttack;
    private List<IDamagable> _toDamage;

    private AudioSource _myAudioSource;

    private void Awake()
    {
        _canAttack = true;
        _toDamage = new List<IDamagable>();
        _myAudioSource = GetComponent<AudioSource>();
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

        _toDamage.ForEach(damagable => damagable.DealDamage(_damageAmount));

        PlayDamageSounds();

        yield return new WaitForSeconds(_offsetBetweenDealingDamage);

        _canAttack = true;
    }

    private void PlayDamageSounds()
    {
        _myAudioSource.pitch = Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();
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
