using UnityEngine;

public class ProjectilePropertiesHandler : MonoBehaviour
{
    [SerializeField] ProjectileObjectPool.ProjectileType _projectileType;
    [SerializeField] string _projectileLayerName = "Player Projectile";

    private void Start()
    {
        Weapon weapon = GetComponent<Weapon>();
        weapon.SetProjectileType(_projectileType);
        weapon.SetProjectileLayerName(_projectileLayerName);
    }
}
