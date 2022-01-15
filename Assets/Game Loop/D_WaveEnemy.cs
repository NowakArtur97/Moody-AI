using UnityEngine;
using static WaveSpawner;

[CreateAssetMenu(fileName = "_WaveEnemy", menuName = "Wave Enemy Data")]
public class D_WaveEnemy : ScriptableObject
{
    // TODO: D_WaveEnemy: REMOVE
    public GameObject enemyPrefab;

    public int enemySpawnPoints = 1;

    public EnemyType enemyType;
}
