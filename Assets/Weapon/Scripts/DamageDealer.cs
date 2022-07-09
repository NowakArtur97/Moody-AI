using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DamageDealer : MonoBehaviour
{
    private readonly string DAMAGE_TRIGGER = "damage";

    [SerializeField] private float _damageAmount = 5.0f;
    [SerializeField] float _offsetBetweenDealingDamage = 0.5f;
    [SerializeField] float _minSoundPitch = 0.95f;
    [SerializeField] float _maxSoundPitch = 1.05f;

    private bool _canAttack;
    private List<IDamagable> _toDamage;

    private AudioSource _myAudioSource;
    private Animator _myAnimatior;

    private void Awake()
    {
        _canAttack = true;
        _toDamage = new List<IDamagable>();
        _myAudioSource = GetComponent<AudioSource>();
        _myAnimatior = gameObject.transform.parent.GetComponentInChildren<Animator>();
    }

    private void Update() => DealDamage();

    private void DealDamage()
    {
        if (_canAttack && _toDamage.Count > 0)
        {
            Debug.Log(3);
            _myAnimatior.SetTrigger(DAMAGE_TRIGGER);
        }
    }

    public void DamageTrigger() => StartCoroutine(DamageCoroutine());

    private IEnumerator DamageCoroutine()
    {
        _canAttack = false;

        _toDamage.ForEach(damagable => damagable.DealDamage(_damageAmount));

        PlayDamageSounds();
        Debug.Log(4);

        yield return new WaitForSeconds(_offsetBetweenDealingDamage);

        Debug.Log(5);
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
        Debug.Log(1);
        Debug.Log(collision.gameObject.name);
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
