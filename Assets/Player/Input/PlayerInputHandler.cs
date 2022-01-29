using System;
using UnityEngine;
using static CameraShake;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private SpaceMovementController _spaceMovementController;
    private WeaponSystem _weaponSystem;
    private bool _mouseInput;
    private bool _canShoot;
    private WaveManager _waveManager;
    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += EnableShooting;
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += DisableShooting;
    }

    private void Start()
    {
        _spaceMovementController = GetComponent<SpaceMovementController>();
        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
        _weaponSystem = transform.parent.GetComponentInChildren<WeaponSystem>();
    }

    private void OnEnable() => _canShoot = true;

    private void OnDestroy()
    {
        _waveManager.OnStartWave -= EnableShooting;
        _waveSpawner.OnFinishWave -= DisableShooting;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
    }

    private void HandleMovementInput() => _spaceMovementController.SetMovementVector(_playerInputManager.MovementInput);

    private void HandleShootingInput()
    {
        if (_canShoot)
        {
            _mouseInput = _playerInputManager.MouseInput;

            _weaponSystem.CurentWeapon.IsShooting(_mouseInput);

            if (_mouseInput && _weaponSystem.CurentWeapon.CanShoot)
            {
                CameraShakeInstance.Shake();
            }
        }
    }

    private void EnableShooting(int waveNumber) => _canShoot = true;

    private void DisableShooting()
    {
        _canShoot = false;
        _weaponSystem.CurentWeapon.IsShooting(false);
    }
}
