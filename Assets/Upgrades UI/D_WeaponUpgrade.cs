using UnityEngine;
using static ProjectileObjectPool;

[CreateAssetMenu(fileName = "_WeaponUpgrade", menuName = "Weapon Upgrade Data")]
public class D_WeaponUpgrade : ScriptableObject
{
    public ProjectileType projetileType;
    public bool isUnlockedAtTheBegining = false;

    public float maximallyUpgradedDamage = 70.0f;
    public float damageUpgradeStep = 10.0f;

    public float maximallyUpgradedFiringSpeed = 0.07f;
    public float firingSpeedUpgradeStep = -0.02f;

    public float maximallyUpgradedAmmoConsumption = 10.0f;
    public float ammoConsumptionUpgradeStep = -2.0f;

    public float maximallyUpgradedMovementSpeed = 30.0f;
    public float movementSpeedUpgradeStep = 2.0f;
}
