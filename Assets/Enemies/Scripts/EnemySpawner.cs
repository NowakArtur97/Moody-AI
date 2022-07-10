using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private D_WaveEnemy _enemyToSpawnData;
    [SerializeField] private float _timeBetweenSpawning = 3.0f;

    private WaveSpawner _waveSpawner;
    private EnemyHealthSystem _enemyHealthSystem;

    private bool _isSpawning;

    private void Awake()
    {
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _enemyHealthSystem = GetComponentInChildren<EnemyHealthSystem>();
    }

    private void OnEnable() => _isSpawning = false;

    private void Update()
    {
        if (!_isSpawning)
        {
            StartCoroutine(SpawnEnemyCoroutine());
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        _isSpawning = true;

        yield return new WaitForSeconds(_timeBetweenSpawning);

        if (!_enemyHealthSystem.IsDying)
        {
            _waveSpawner.SpawnEnemy(_enemyToSpawnData, transform.position);
        }

        _isSpawning = false;
    }
}
