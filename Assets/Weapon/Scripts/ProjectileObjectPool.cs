using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private int _spawnAmount = 50;
    [SerializeField] private GameObject _defaultBulletPrefab;
    [SerializeField] private GameObject _ballProjectilePrefab;

    private GameObject _projectilePrefab
    {
        get
        {
            switch (_projectileType)
            {
                case ProjectileType.DEFAULT_BULLET:
                    return _defaultBulletPrefab;
                case ProjectileType.BALL_PROJECTILE:
                    return _ballProjectilePrefab;
                default: return _defaultBulletPrefab;
            }
        }
    }
    private Transform _projectileSpawnPosition;
    private Quaternion _projectileRotation;
    private Vector2 _projectileDirectionVector;

    public static ProjectileObjectPool ProjectileObjectPoolInstance { get; private set; }

    public enum ProjectileType { DEFAULT_BULLET, BALL_PROJECTILE }

    private ObjectPool<GameObject> _projectileObjectPool;

    private ProjectileType _projectileType;

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

        SetupProjectileObjectPool();
    }

    private void SetupProjectileObjectPool()
    {
        _projectileObjectPool = new ObjectPool<GameObject>(
                () =>
                {
                    GameObject projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, _projectileRotation);
                    projectile.GetComponent<Projectile>().SetDirection(_projectileDirectionVector);
                    return projectile;
                },
                projectile => projectile.gameObject.SetActive(true),
                projectile => projectile.gameObject.SetActive(false),
                projectile => Destroy(projectile.gameObject),
                true,
                _spawnAmount
            );
    }

    public GameObject InstantiateProjectile(ProjectileType projectileType, Transform projectileSpawnPosition, Quaternion projectileRotation,
        Vector2 projectileDirectionVector)
    {
        _projectileType = projectileType;
        _projectileSpawnPosition = projectileSpawnPosition;
        _projectileRotation = projectileRotation;
        _projectileDirectionVector = projectileDirectionVector;
        return _projectileObjectPool.Get();
    }

    public void ReleaseProjectile(GameObject projectile) => _projectileObjectPool.Release(projectile);
}
