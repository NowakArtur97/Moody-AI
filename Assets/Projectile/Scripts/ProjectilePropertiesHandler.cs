using UnityEngine;

public class ProjectilePropertiesHandler : MonoBehaviour
{
    [SerializeField] ProjectileObjectPool.ProjectileType _projectileType;

    private void Awake() => GetComponent<Weapon>().SetProjectileType(_projectileType);
}
