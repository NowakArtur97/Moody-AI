using UnityEngine;
using static EnemyRandomTargetTransformManager;

public class EnemyTargetTransformHandler : MonoBehaviour
{
    [SerializeField] private TargetType _targetType = TargetType.PLANET;

    public Transform TargetTransform { get; private set; }

    private void Start() => TargetTransform = FindObjectOfType<EnemyRandomTargetTransformManager>().GetRandomTransform(_targetType);
}