using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class HealthSystem : MonoBehaviour, IDamagable
{
    private readonly string EXPLOSION_TRIGGER = "explode";
    private readonly string EMPTY_LAYER = "Empty";

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
    private int _defaultLayer;

    public bool IsDying { get; private set; }

    public float CurrentHealth;

    protected virtual void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myAnimator = transform.parent.GetComponentInChildren<Animator>();
        _mySpriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();

        _defaultLayer = transform.parent.gameObject.layer;
    }

    private void OnEnable()
    {
        IsDying = false;
        CurrentHealth = _maxHealth;

        transform.parent.gameObject.layer = _defaultLayer;
    }

    public virtual void DealDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0 && !IsDying)
        {
            transform.parent.gameObject.layer = LayerMask.NameToLayer(EMPTY_LAYER);

            IsDying = true;

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
