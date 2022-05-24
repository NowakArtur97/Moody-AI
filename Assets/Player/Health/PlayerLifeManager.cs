using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int _numberOfPlayerLives = 3;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _healthUI;
    [SerializeField] private GameObject _healthWrapper;

    private List<GameObject> _playerLivesUI;
    private GameObject _playerGameObject;
    private PlayerHealthSystem _lastPlayerObjectHealthSystem;

    public Action OnPlayerRespawn;

    private void Awake()
    {
        _playerLivesUI = new List<GameObject>();
        _numberOfPlayerLives++;
    }

    private void Start()
    {
        SpawnHealth();
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
        _lastPlayerObjectHealthSystem = _playerGameObject.GetComponentInChildren<PlayerHealthSystem>();
        _lastPlayerObjectHealthSystem.OnPlayerDeath += SpawnPlayer;
        _numberOfPlayerLives--;

        int lastLiveIndex = _playerLivesUI.Count - 1;
        Destroy(_playerLivesUI[lastLiveIndex]);
        _playerLivesUI.RemoveAt(lastLiveIndex);

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

    private void SpawnHealth()
    {
        for (int index = 0; index < _numberOfPlayerLives; index++)
        {
            GameObject playerHealth = Instantiate(_healthUI, Vector2.zero, Quaternion.identity);
            playerHealth.transform.parent = _healthWrapper.transform;
            playerHealth.transform.localScale = Vector3.one;
            _playerLivesUI.Add(playerHealth);
        }
    }
}
