using System.Collections;
using UnityEngine;
using static ProjectileObjectPool;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    private float _movementVelocity = 20.0f;
    protected float MovementVelocity
    {
        get { return _movementVelocity; }
        set { _movementVelocity = value; }
    }
    [SerializeField] private float _damageAmount = 10.0f;
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] float _minSoundPitch = 0.95f;
    [SerializeField] float _maxSoundPitch = 1.05f;
    [SerializeField] float _timeBeforeReleasing = 30f;

    private AudioSource _myAudioSource;

    private void Awake() => _myAudioSource = GetComponent<AudioSource>();

    private void OnEnable() => StartCoroutine(ReleaseCoroutine(_timeBeforeReleasing));

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponentInChildren<IDamagable>()?.DealDamage(_damageAmount);

        PlayHitSound();

        StartCoroutine(ReleaseCoroutine(_myAudioSource.clip.length));
    }

    private void PlayHitSound()
    {
        _myAudioSource.pitch = Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();
    }

    private IEnumerator ReleaseCoroutine(float timeToRelease)
    {
        yield return new WaitForSeconds(timeToRelease);

        ProjectileObjectPoolInstance.ReleaseProjectile(gameObject, _projectileType);
    }
}
