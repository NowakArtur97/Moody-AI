using Pathfinding;
using UnityEngine;

public class TargetToAIPath : MonoBehaviour
{
    protected EnemyTargetTransformHandler EnemyTargetTransformHandler { get; private set; }
    protected AIDestinationSetter AIDestinationSetter { get; private set; }

    protected virtual void Awake()
    {
        EnemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Update() => SetAITarget();

    protected virtual void SetAITarget()
    {
        if (AIDestinationSetter.target == null)
        {
            AIDestinationSetter.target = EnemyTargetTransformHandler.FindRandomTargetTransform();
        }
    }
}
