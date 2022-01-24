using UnityEngine;
using static ProjectileObjectPool;

[CreateAssetMenu(fileName = "_WeaponUpgrade", menuName = "Weapon Upgrade Data")]
public class D_WeaponUpgrade : ScriptableObject
{
    // TODO: D_WeaponUpgrade: Check if used
    public ProjectileType projetileType;

    public float startingDamage = 20.0f;
    public float damageUpgradeStep = 10.0f;

    public float startingFiringSpeed = 0.15f;
    public float firingSpeedUpgradeStep = -0.02f;

    public float startingCost = 20.0f;
    public float costUpgradeStep = -2.0f;

    public float startingMovementSpeed = 20.0f;
    public float movementSpeedUpgradeStep = 2.0f;
}
