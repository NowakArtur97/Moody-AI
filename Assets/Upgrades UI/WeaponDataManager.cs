using UnityEngine;
using static ProjectileObjectPool;

public class WeaponDataManager : MonoBehaviour
{
    [SerializeField] private D_WeaponUpgrade _startingData;

    private ProjectileType _projetileType;
    public float CurrentDamage { get; private set; }
    public float CurrentFiringSpeed { get; private set; }
    public float CurrentCost { get; private set; }
    public float CurrentMovementSpeed { get; private set; }

    private void Awake()
    {
        _projetileType = _startingData.projetileType;
        CurrentDamage = _startingData.startingDamage;
        CurrentFiringSpeed = _startingData.startingFiringSpeed;
        CurrentCost = _startingData.startingCost;
        CurrentMovementSpeed = _startingData.startingMovementSpeed;
    }

    public void UpgradeDamage() => CurrentDamage += _startingData.damageUpgradeStep;

    public void UpgradeFiringSpeed() => CurrentFiringSpeed += _startingData.firingSpeedUpgradeStep;

    public void UpgradeCost() => CurrentCost += _startingData.costUpgradeStep;

    public void UpgradeMovementSpeed() => CurrentMovementSpeed += _startingData.movementSpeedUpgradeStep;
}
