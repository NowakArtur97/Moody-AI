using UnityEngine;

public class ProjectilePropertiesHandler : MonoBehaviour
{
    [SerializeField] ProjectileObjectPool.ProjectileType _projectileType;
    [SerializeField] string _projectileLayerName = "Player Projectile";

    private void Start()
    {
        Weapon _weapon = GetComponent<Weapon>();
        _weapon.SetProjectileType(_projectileType);
        _weapon.SetProjectileLayerName(_projectileLayerName);
    }
}
