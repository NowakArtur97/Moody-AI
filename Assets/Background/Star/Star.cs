using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Star : MonoBehaviour
{
    [SerializeField] private float _minTimeBeforeTwinkle = 3.0f;
    [SerializeField] private float _maxTimeBeforeTwinkle = 10.0f;

    private bool _canTwinkle;

    private Animator _myAnimator;

    private void Awake() => _myAnimator = GetComponent<Animator>();

    private void Update() => Twinkle();

    private void Twinkle()
    {
        if (_canTwinkle)
        {
            StartCoroutine(TwinkleCoroutine());
        }
    }

    private IEnumerator TwinkleCoroutine()
    {
        _canTwinkle = false;

        _myAnimator.SetTrigger("twinkle");

        yield return new WaitForSeconds(Random.Range(_minTimeBeforeTwinkle, _maxTimeBeforeTwinkle));

        _canTwinkle = true;
    }
}
