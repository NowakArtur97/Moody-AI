using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private string[] _cameraShakeBoolAnimations = { "shake_01", "shake_02", "shake_03" };

    public static CameraShake CameraShakeInstance { get; private set; }

    private Animator _myAnimator;

    private void Awake()
    {
        if (CameraShakeInstance != null && CameraShakeInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            CameraShakeInstance = this;
        }

        _myAnimator = GetComponent<Animator>();
    }

    public void Shake() => _myAnimator.SetTrigger(_cameraShakeBoolAnimations[Random.Range(0, _cameraShakeBoolAnimations.Length)]);
}
