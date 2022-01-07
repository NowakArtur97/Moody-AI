using UnityEngine;

public class ProjectileTypeHandler : MonoBehaviour
{
    [SerializeField] ProjectileObjectPool.ProjectileType _projectileType;

    private void Start() => GetComponent<Weapon>().SetProjectileType(_projectileType);
}
