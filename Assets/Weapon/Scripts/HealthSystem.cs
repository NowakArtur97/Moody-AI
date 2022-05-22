using System.Collections;
using UnityEngine;
using static EnemyObjectPool;
using static AmmoRestorationManager;
using System;

[RequireComponent(typeof(AudioSource))]
public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth = 40.0f;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] float _minSoundPitch = 0.8f;
    [SerializeField] float _maxSoundPitch = 1.05f;
    [SerializeField] bool _isEnemy = true;
    [SerializeField] bool _isPlanet = false;

    private AudioSource _myAudioSource;
    private bool _isDying;

    public float CurrentHealth { get; private set; }

    public Action OnPlayerDeath;

    private void Awake() => _myAudioSource = GetComponent<AudioSource>();

    private void OnEnable()
    {
        _isDying = false;
        CurrentHealth = _maxHealth;
    }

    public void DealDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0 && !_isDying)
        {
            _isDying = true;

            PlayDeathSound();

            StartCoroutine(ReleaseCoroutine());
        }

        HandleAmmoRestorationActions();
    }

    private void PlayDeathSound()
    {
        _myAudioSource.pitch = UnityEngine.Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();
    }

    private IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(_myAudioSource.clip.length);

        if (_isEnemy)
        {
            AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DEFEATING_ENEMIES);

            EnemyObjectPoolInstance.ReleaseEnemy(transform.parent.gameObject, _enemyType);
        }
        else if (!_isPlanet)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    private void HandleAmmoRestorationActions()
    {
        if (_isPlanet)
        {
            AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DAMAGING_PLANET);
        }
        else if (!_isEnemy)
        {
            AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.TAKING_DAMAGE);
        }
    }
}
