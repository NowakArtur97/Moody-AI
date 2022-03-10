using System;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int _numberOfPlayerLives = 3;
    [SerializeField] private GameObject _playerPrefab;

    private GameObject _playerGameObject;
    private HealthSystem _lastPlayerObjectHealthSystem;

    public Action OnPlayerRespawn;

    private void Start()
    {
        _numberOfPlayerLives++;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (_lastPlayerObjectHealthSystem)
        {
            _lastPlayerObjectHealthSystem.OnPlayerDeath -= SpawnPlayer;
        }
        // TODO: Animation for Player Death
        Destroy(_playerGameObject);
        _playerGameObject = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity);
        _lastPlayerObjectHealthSystem = _playerGameObject.GetComponentInChildren<HealthSystem>();
        _lastPlayerObjectHealthSystem.OnPlayerDeath += SpawnPlayer;
        _numberOfPlayerLives--;

        if (_numberOfPlayerLives <= 0)
        {
            // TODO: Add Game Over screen
            Debug.Log("GAME OVER");
        }
        else
        {
            OnPlayerRespawn?.Invoke();
        }
    }
}
