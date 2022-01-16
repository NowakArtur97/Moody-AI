using System.Collections;
using UnityEngine;
using static ProjectileObjectPool;

[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _movementVelocity = 20.0f;
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] float _minSoundPitch = 0.95f;
    [SerializeField] float _maxSoundPitch = 1.05f;
    [SerializeField] float _timeBeforeReleasing = 4f;

    private Vector3 _projectileDirection;

    private AudioSource _myAudioSource;

    private void Awake() => _myAudioSource = GetComponent<AudioSource>();

    private void OnEnable() => StartCoroutine(ReleaseCoroutine(_timeBeforeReleasing));

    private void Update() => transform.position += _projectileDirection * Time.deltaTime * _movementVelocity;

    public void SetDirection(Vector3 projectileDirection)
    {
        _projectileDirection = projectileDirection;
        _projectileDirection = new Vector3(_projectileDirection.x, _projectileDirection.y, 0.0f);
        float angleCorrection = Mathf.Atan2(_projectileDirection.y, _projectileDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleCorrection, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
