using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnemyObjectPool;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private D_WaveEnemy[] _enemiesData;
    [SerializeField] private int _startingSpawnPoints = 10;
    [SerializeField] private int _spawnPointsMultiplier = 1;
    [SerializeField] private int _spawningRadius = 30;
    [SerializeField] private float _timeBetweenSpawningEnemies = 1;
    [SerializeField] private float _timeBeforeSpawningEnemies = 2;
    [SerializeField] private float _timeAfterSpawningEnemies = 2;

    public Action OnFinishWave;

    private List<GameObject> _spawnedEnemies;
    private bool _isSpawning;
    private WaveManager _waveManager;
    private bool _isFinishingWave;

    private void Awake()
    {
        _spawnedEnemies = new List<GameObject>();
        _isSpawning = true;
        _isFinishingWave = false;

        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += StartSpawning;
    }

    private void OnDestroy() => _waveManager.OnStartWave -= StartSpawning;

    private void Update()
    {
        if (!_isSpawning && !_isFinishingWave && _spawnedEnemies.All(enemy => !enemy.activeInHierarchy))
        {
            StartCoroutine(FinishWave());
        }
    }

    private void StartSpawning(int numberOfWave)
    {
        int spawnPoints = _startingSpawnPoints + numberOfWave * _spawnPointsMultiplier;
        StartCoroutine(SpawnEnemiesCoroutine(spawnPoints));
    }

    private IEnumerator SpawnEnemiesCoroutine(int spawnPoints)
    {
        _isSpawning = true;
        _isFinishingWave = false;

        _spawnedEnemies.Clear();

        yield return new WaitForSeconds(_timeBeforeSpawningEnemies);

        while (_enemiesData.Any(enemydata => enemydata.enemySpawnPoints <= spawnPoints))
        {
            D_WaveEnemy chosenEnemyData = _enemiesData[UnityEngine.Random.Range(0, _enemiesData.Length)];

            if (chosenEnemyData.enemySpawnPoints <= spawnPoints)
            {
                spawnPoints -= chosenEnemyData.enemySpawnPoints;

                SpawnEnemy(chosenEnemyData);

                yield return new WaitForSeconds(_timeBetweenSpawningEnemies);
            }
        }

        _isSpawning = false;
    }

    private void SpawnEnemy(D_WaveEnemy chosenEnemyData) => _spawnedEnemies.Add(
        EnemyObjectPoolInstance.InstantiateEnemy(chosenEnemyData.enemyType, GetRandomPositionInRadius()));

    private Vector2 GetRandomPositionInRadius()
    {
        float randomAngle = UnityEngine.Random.value * 360;
        return new Vector2(_spawningRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad), _spawningRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad));
    }

    private IEnumerator FinishWave()
    {
        _isFinishingWave = true;

        yield return new WaitForSeconds(_timeAfterSpawningEnemies);

        OnFinishWave?.Invoke();
    }
}
