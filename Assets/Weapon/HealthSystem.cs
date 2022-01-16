using System.Collections;
using UnityEngine;
using static EnemyObjectPool;

[RequireComponent(typeof(AudioSource))]
public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private float _health = 40.0f;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] float _minSoundPitch = 0.8f;
    [SerializeField] float _maxSoundPitch = 1.05f;
    [SerializeField] bool _isEnemy = true;

    private AudioSource _myAudioSource;
    private bool _isDying;
    [SerializeField] private float _currentHealth;

    private void Awake() => _myAudioSource = GetComponent<AudioSource>();

    private void OnEnable()
    {
        _isDying = false;
        _currentHealth = _health;
    }

    public void DealDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0 && !_isDying)
        {
            _isDying = true;

            PlayDeathSound();

            StartCoroutine(ReleaseCoroutine());
        }
    }

    private void PlayDeathSound()
    {
        _myAudioSource.pitch = Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();
    }

    private IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(_myAudioSource.clip.length);

        if (_isEnemy)
        {
            EnemyObjectPoolInstance.ReleaseEnemy(transform.parent.gameObject, _enemyType);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
