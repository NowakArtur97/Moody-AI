using System.Collections;
using System.Linq;
using UnityEngine;
using static ProjectileObjectPool;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseProjectile : MonoBehaviour
{
    private const string EXPLOSION_TRIGGER = "explode";

    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] private bool _isEnemy;
    [SerializeField] private float _minSoundPitch = 0.95f;
    [SerializeField] private float _maxSoundPitch = 1.05f;
    [SerializeField] private float _timeBeforeReleasing = 30f;
    [SerializeField] private bool _isDestructable = true;

    private AudioSource _myAudioSource;
    private Animator _myAnimator;
    protected WeaponDataManager WeaponDataManager { get; private set; }

    protected bool IsMoving;
    protected Vector3 ProjectileDirection { get; private set; }
    private float _hitSoundStartTime;

    protected virtual void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myAnimator = GetComponentInChildren<Animator>();
    }

    private void Start() => WeaponDataManager = FindObjectsOfType<WeaponDataManager>()
            .First(wdm => wdm.ProjectileType == _projectileType && wdm.IsEnemy == _isEnemy);

    protected virtual void OnEnable()
    {
        StartCoroutine(ReleaseCoroutine(_timeBeforeReleasing));
        IsMoving = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        collision?.gameObject.GetComponentInChildren<IDamagable>()?.DealDamage(WeaponDataManager.CurrentDamage);

        IsMoving = false;

        PlayHitSound();

        if (_isDestructable)
        {
            _myAnimator.SetTrigger(EXPLOSION_TRIGGER);
        }
    }

    public void SetDirection(Vector3 projectileDirection)
    {
        ProjectileDirection = projectileDirection;
        ProjectileDirection = new Vector3(ProjectileDirection.x, ProjectileDirection.y, 0.0f);
        float angleCorrection = Mathf.Atan2(ProjectileDirection.y, ProjectileDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleCorrection, Vector3.forward);
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
