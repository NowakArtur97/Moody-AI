using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private int _spawnAmount = 5;
    [SerializeField] private GameObject _jellyfishPrefab;
    [SerializeField] private GameObject _teetherPrefab;
    [SerializeField] private GameObject _batPrefab;
    [SerializeField] private GameObject _stingrayPrefab;
    [SerializeField] private GameObject _boomerPrefab;
    [SerializeField] private GameObject _wormPrefab;

    private Vector2 _enemySpawnPosition;

    public static EnemyObjectPool EnemyObjectPoolInstance { get; private set; }

    public enum EnemyType { JELLYFISH, TEETHER, BAT, STINGRAY, BOOMER, WORM }

    private ObjectPool<GameObject> _jellyfishObjectPool;
    private ObjectPool<GameObject> _teetherObjectPool;
    private ObjectPool<GameObject> _batObjectPool;
    private ObjectPool<GameObject> _stingrayObjectPool;
    private ObjectPool<GameObject> _boomerObjectPool;
    private ObjectPool<GameObject> _wormObjectPool;

    private void Awake()
    {
        if (EnemyObjectPoolInstance != null && EnemyObjectPoolInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            EnemyObjectPoolInstance = this;
        }

        SetupEnemyObjectPools();
    }

    private void SetupEnemyObjectPools()
    {
        _jellyfishObjectPool = CreateEnemyObjectPool(_jellyfishPrefab);
        _teetherObjectPool = CreateEnemyObjectPool(_teetherPrefab);
        _batObjectPool = CreateEnemyObjectPool(_batPrefab);
        _stingrayObjectPool = CreateEnemyObjectPool(_stingrayPrefab);
        _boomerObjectPool = CreateEnemyObjectPool(_boomerPrefab);
        _wormObjectPool = CreateEnemyObjectPool(_wormPrefab);
    }

    private ObjectPool<GameObject> CreateEnemyObjectPool(GameObject enemyPrefab) => new ObjectPool<GameObject>(
                () =>
                {
                    GameObject enemy = Instantiate(enemyPrefab, _enemySpawnPosition, Quaternion.identity);
                    gameObject.transform.parent = transform;
                    return enemy;
                },
                enemy =>
                {
                    enemy.gameObject.SetActive(true);
                    enemy.transform.position = _enemySpawnPosition;
                    gameObject.transform.parent = transform;
                },
                enemy => enemy.gameObject.SetActive(false),
                enemy => Destroy(enemy.gameObject),
                true,
                _spawnAmount
            );

    public GameObject InstantiateEnemy(EnemyType enemyType, Vector3 enemySpawnPosition)
    {
        _enemySpawnPosition = enemySpawnPosition;

        switch (enemyType)
        {
            case EnemyType.TEETHER:
                return _teetherObjectPool.Get();
            case EnemyType.BAT:
                return _batObjectPool.Get();
            case EnemyType.STINGRAY:
                return _stingrayObjectPool.Get();
            case EnemyType.BOOMER:
                return _boomerObjectPool.Get();
            case EnemyType.WORM:
                return _wormObjectPool.Get();
            default:
                return _jellyfishObjectPool.Get();
        }
    }

    public void ReleaseEnemy(GameObject enemy, EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.TEETHER:
                _teetherObjectPool.Release(enemy);
                break;
            case EnemyType.BAT:
                _batObjectPool.Release(enemy);
                break;
            case EnemyType.STINGRAY:
                _stingrayObjectPool.Release(enemy);
                break;
            case EnemyType.BOOMER:
                _boomerObjectPool.Release(enemy);
                break;
            case EnemyType.WORM:
                _wormObjectPool.Release(enemy);
                break;
            default:
                _jellyfishObjectPool.Release(enemy);
                break;
        }
    }
}
