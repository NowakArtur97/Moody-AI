using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public abstract class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth = 40.0f;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    [SerializeField] float _minSoundPitch = 0.8f;
    [SerializeField] float _maxSoundPitch = 1.05f;

    protected AudioSource MyAudioSource { get; private set; }
    private bool _isDying;

    public float CurrentHealth { get; private set; }

    private void Awake() => MyAudioSource = GetComponent<AudioSource>();

    private void OnEnable()
    {
        _isDying = false;
        CurrentHealth = _maxHealth;
    }

    public virtual void DealDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0 && !_isDying)
        {
            _isDying = true;

            PlayDeathSound();

            StartCoroutine(ReleaseCoroutine());
        }
    }

    private void PlayDeathSound()
    {
        MyAudioSource.pitch = UnityEngine.Random.Range(_minSoundPitch, _maxSoundPitch);
        MyAudioSource.Play();
    }

    protected abstract IEnumerator ReleaseCoroutine();
}
