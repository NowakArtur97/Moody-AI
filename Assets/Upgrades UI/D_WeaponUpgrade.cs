using UnityEngine;
using static ProjectileObjectPool;

[CreateAssetMenu(fileName = "_WeaponUpgrade", menuName = "Weapon Upgrade Data")]
public class D_WeaponUpgrade : ScriptableObject
{
    public ProjectileType projetileType;
    public bool isUnlockedAtTheBegining = false;
    public int unlockCost = 50;

    public float maximallyUpgradedDamage = 70.0f;
    public float damageUpgradeStep = 10.0f;
    public int startingDamageUpgradeCost = 10;
    public int damageUpgradeCostStep = 10;

    public float maximallyUpgradedFiringSpeed = 0.07f;
    public float firingSpeedUpgradeStep = -0.02f;
    public int startingFiringSpeedUpgradeCost = 10;
    public int firingSpeedUpgradeCostStep = 10;

    public float maximallyUpgradedAmmoConsumption = 10.0f;
    public float ammoConsumptionUpgradeStep = -2.0f;
    public int startingAmmoConsumptionUpgradeCost = 10;
    public int ammoConsumptionUpgradeCostStep = 10;

    public float maximallyUpgradedMovementSpeed = 30.0f;
    public float movementSpeedUpgradeStep = 2.0f;
    public int startingMovementSpeedUpgradeCost = 10;
    public int movementSpeedUpgradeCostStep = 10;
}
