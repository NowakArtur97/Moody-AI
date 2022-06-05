using Pathfinding;
using UnityEngine;

public class TargetToAIPath : MonoBehaviour
{
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;
    private AIDestinationSetter _aIDestinationSetter;

    private void Awake()
    {
        _enemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Update() => SetAITarget();

    private void SetAITarget()
    {
        if (_aIDestinationSetter.target == null)
        {
            _aIDestinationSetter.target = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }
    }
}
