using UnityEngine;
using static EnemyObjectPool;

[CreateAssetMenu(fileName = "_WaveEnemy", menuName = "Wave Enemy Data")]
public class D_WaveEnemy : ScriptableObject
{
    public int enemySpawnPoints = 1;

    public EnemyType enemyType;
}
