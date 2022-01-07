using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private int _spawnAmount = 50;
    [SerializeField] private GameObject _defaultBulletPrefab;
    [SerializeField] private GameObject _ballProjectilePrefab;

    private Transform _projectileSpawnPosition;
    private Quaternion _projectileRotation;
    private Vector2 _projectileDirectionVector;

    public static ProjectileObjectPool ProjectileObjectPoolInstance { get; private set; }

    public enum ProjectileType { DEFAULT_BULLET, BALL_PROJECTILE }

    private ObjectPool<GameObject> _defaultBulletObjectPool;
    private ObjectPool<GameObject> _ballProjectileObjectPool;

    private void Awake()
    {
        if (ProjectileObjectPoolInstance != null && ProjectileObjectPoolInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            ProjectileObjectPoolInstance = this;
        }

        SetupProjectileObjectPools();
    }

    private void SetupProjectileObjectPools()
    {
        _defaultBulletObjectPool = CreateProjectileObjectPool(_defaultBulletPrefab);
        _ballProjectileObjectPool = CreateProjectileObjectPool(_ballProjectilePrefab);
    }

    private ObjectPool<GameObject> CreateProjectileObjectPool(GameObject projectilePrefab) => new ObjectPool<GameObject>(
                () =>
                {
                    GameObject projectile = Instantiate(projectilePrefab, _projectileSpawnPosition.position, _projectileRotation);
                    projectile.GetComponent<Projectile>().SetDirection(_projectileDirectionVector);
                    return projectile;
                },
                projectile =>
                {
                    projectile.gameObject.SetActive(true);
                    projectile.transform.position = _projectileSpawnPosition.position;
                    projectile.transform.rotation = _projectileRotation;
                    projectile.transform.rotation = _projectileRotation;
                    projectile.GetComponent<Projectile>().SetDirection(_projectileDirectionVector);
                },
                projectile => projectile.gameObject.SetActive(false),
                projectile => Destroy(projectile.gameObject),
                true,
                _spawnAmount
            );

    public GameObject InstantiateProjectile(ProjectileType projectileType, Transform projectileSpawnPosition, Quaternion projectileRotation,
        Vector2 projectileDirectionVector)
    {
        _projectileSpawnPosition = projectileSpawnPosition;
        _projectileRotation = projectileRotation;
        _projectileDirectionVector = projectileDirectionVector;
        switch (projectileType)
        {
            case ProjectileType.BALL_PROJECTILE:
                return _ballProjectileObjectPool.Get();
            default:
                return _defaultBulletObjectPool.Get();
        }
    }

    public void ReleaseProjectile(GameObject projectile, ProjectileType projectileType)
    {
        switch (projectileType)
        {
            case ProjectileType.BALL_PROJECTILE:
                _ballProjectileObjectPool.Release(projectile);
                break;
            default:
                _defaultBulletObjectPool.Release(projectile);
                break;
        }
    }
}
