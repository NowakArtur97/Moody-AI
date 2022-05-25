using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class HealthSystem : MonoBehaviour, IDamagable
{
    private readonly string EXPLOSION_TRIGGER = "explode";

    [SerializeField] private float _maxHealth = 40.0f;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    [SerializeField] float _minSoundPitch = 0.8f;
    [SerializeField] float _maxSoundPitch = 1.05f;

    private AudioSource _myAudioSource;
    private Animator _myAnimator;
    private SpriteRenderer _mySpriteRenderer;
    private bool _isDying;

    public float CurrentHealth { get; private set; }

    protected virtual void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myAnimator = transform.parent.GetComponentInChildren<Animator>();
        _mySpriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();
    }

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

            _myAnimator.SetTrigger(EXPLOSION_TRIGGER);
        }
    }

    private void PlayDeathSound()
    {
        _myAudioSource.pitch = Random.Range(_minSoundPitch, _maxSoundPitch);
        _myAudioSource.Play();
    }

    public abstract void DeathTrigger();
}
