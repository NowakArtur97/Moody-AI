using UnityEngine;
using static EnemyRandomTargetTransformManager;

public class EnemyTargetTransformHandler : MonoBehaviour
{
    [SerializeField] private TargetType _targetType = TargetType.PLANET;

    private EnemyRandomTargetTransformManager _enemyRandomTargetTransformManager;

    private void Awake() => _enemyRandomTargetTransformManager = FindObjectOfType<EnemyRandomTargetTransformManager>();

    public Transform FindRandomTargetTransform() => _enemyRandomTargetTransformManager.GetRandomTransform(_targetType);
}