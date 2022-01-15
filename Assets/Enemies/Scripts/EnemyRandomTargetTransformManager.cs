using UnityEngine;
public class EnemyRandomTargetTransformManager : MonoBehaviour
{
    [SerializeField] private Transform[] _targetTransforms;

    // TODO: RandomTargetTransformEnemyManager: Add more targets with every ten waves
    public Transform GetRandomTransform() => _targetTransforms[0];
}