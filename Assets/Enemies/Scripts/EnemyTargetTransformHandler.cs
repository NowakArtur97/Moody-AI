using UnityEngine;

public class EnemyTargetTransformHandler : MonoBehaviour
{
    public Transform TargetTransform { get; private set; }

    private void Start() => TargetTransform = FindObjectOfType<EnemyRandomTargetTransformManager>().GetRandomTransform();
}