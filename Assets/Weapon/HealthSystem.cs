using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private float _helath;
    [SerializeField] float _minSoundPitch = 0.8f;
    [SerializeField] float _maxSoundPitch = 1.05f;

    private AudioSource _myAudioSource;

    private void Awake() => _myAudioSource = GetComponent<AudioSource>();

    public void DealDamage(float damageAmount)
    {
        _helath -= damageAmount;

        if (_helath <= 0)
        {
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

        // TODO: HeathSystem: Return to pool
        Destroy(transform.parent.gameObject);
    }
}
