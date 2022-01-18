using System.Collections;
using UnityEngine;
using static ProjectileObjectPool;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseProjectile : MonoBehaviour
{
    private const string EXPLOSION_TRIGGER = "explode";

    [SerializeField] private float _movementVelocity = 20.0f;
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
    private Animator _myAnimator;

    protected bool IsMoving { get; private set; }
    private float _hitSoundStartTime;

    protected virtual void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(ReleaseCoroutine(_timeBeforeReleasing));
        IsMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponentInChildren<IDamagable>()?.DealDamage(_damageAmount);

        IsMoving = false;

        _myAnimator.SetTrigger(EXPLOSION_TRIGGER);

        PlayHitSound();
    }

    private void PlayHitSound()
    {
        _myAudioSource.pitch = Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();
        _hitSoundStartTime = Time.time;
    }

    public void ReleaseTrigger()
    {
        float timeUntillClipEnds = (_hitSoundStartTime + _myAudioSource.clip.length) - Time.time;
        StartCoroutine(ReleaseCoroutine(timeUntillClipEnds > 0 ? timeUntillClipEnds : 0.0f));
    }

    private IEnumerator ReleaseCoroutine(float timeToRelease)
    {
        yield return new WaitForSeconds(timeToRelease);

        ProjectileObjectPoolInstance.ReleaseProjectile(gameObject, _projectileType);
    }
}
