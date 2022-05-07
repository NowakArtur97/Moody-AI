using UnityEngine;

public class PlayerParticleHandler : MonoBehaviour
{
    [SerializeField] private float _particleChangeSpeed = 2.0f;
    [SerializeField] private float _particlesAmountWhenMoving = 500.0f;

    private ParticleSystem _myParticleSystem;
    private ParticleSystem.EmissionModule _myParticleSystemEmissionModule;
    private PlayerInputManager _playerInputManager;

    private float _particleEmissionRate;

    private void Awake()
    {
        _myParticleSystem = GetComponent<ParticleSystem>();
        _myParticleSystemEmissionModule = _myParticleSystem.emission;
        _particleEmissionRate = 0.0f;
        _myParticleSystemEmissionModule.rateOverTime = _particleEmissionRate;

        _playerInputManager = GetComponentInChildren<PlayerInputManager>();
    }

    private void Update()
    {
        if (_playerInputManager.MovementInput != Vector2.zero)
        {
            _particleEmissionRate = Mathf.Lerp(_particleEmissionRate, _particlesAmountWhenMoving, Time.deltaTime * _particleChangeSpeed);
        }
        else
        {
            _particleEmissionRate = Mathf.Lerp(_particleEmissionRate, 0, Time.deltaTime * _particleChangeSpeed);
        }

        _myParticleSystemEmissionModule.rateOverTime = _particleEmissionRate;
    }
}
