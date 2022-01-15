using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private D_WaveEnemy[] _enemiesData;
    [SerializeField] private int _startingSpawnPoints = 10;
    [SerializeField] private int _spawnPointsMultiplier = 1;
    [SerializeField] private int _spawningRadius = 10;
    [SerializeField] private float _timeBetweenSpawningEnemies = 1;
    [SerializeField] private float _timeBeforeSpawningEnemies = 2;
    [SerializeField] private float _timeAfterSpawningEnemies = 2;

    public Action OnFinishWave;

    private List<GameObject> _spawnedEnemies;
    private bool _isSpawning;

    public enum EnemyType { JELLYFISH, TEETHER, BAT, STINGRAY, BOOMER, WORM }

    private void Awake()
    {
        _spawnedEnemies = new List<GameObject>();
        _isSpawning = true;
    }

    private void Start() => FindObjectOfType<WaveManager>().OnStartWave += StartSpawning;

    private void OnDestroy() => FindObjectOfType<WaveManager>().OnStartWave -= StartSpawning;

    private void Update()
    {
        if (!_isSpawning && _spawnedEnemies.All(enemy => !enemy.activeInHierarchy))
        {
            FinishWave();
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

        _spawnedEnemies.Clear();

        yield return new WaitForSeconds(_timeBeforeSpawningEnemies);

        while (_enemiesData.Any(enemydata => enemydata.enemySpawnPoints <= spawnPoints))
        {
            D_WaveEnemy chosenEnemyData = _enemiesData[UnityEngine.Random.Range(0, _enemiesData.Length)];

            if (chosenEnemyData.enemySpawnPoints <= spawnPoints)
            {
                spawnPoints -= chosenEnemyData.enemySpawnPoints;
                Vector2 position = Vector2.zero + UnityEngine.Random.insideUnitCircle * _spawningRadius;

                GameObject enemy = Instantiate(chosenEnemyData.enemyPrefab, position, Quaternion.identity);
                _spawnedEnemies.Add(enemy);

                yield return new WaitForSeconds(_timeBetweenSpawningEnemies);
            }
        }

        _isSpawning = false;
    }

    private IEnumerator FinishWave()
    {
        yield return new WaitForSeconds(_timeAfterSpawningEnemies);

        OnFinishWave?.Invoke();
    }
}
