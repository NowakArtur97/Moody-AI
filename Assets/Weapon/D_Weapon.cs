using UnityEngine;
using static ProjectileObjectPool;

[CreateAssetMenu(fileName = "_Weapon", menuName = "Weapon Data")]
public class D_Weapon : ScriptableObject
{
    public ProjectileType projetileType;

    public float startingDamage = 20.0f;

    public float startingFiringSpeed = 0.15f;

    public float startingAmmoConsumption = 20.0f;

    public float startingMovementSpeed = 20.0f;
}
