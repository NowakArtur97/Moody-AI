public class EnemyTargetToAIPath : TargetToAIPath
{
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;

    protected override void Awake()
    {
        base.Awake();

        _enemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();
    }

    protected override void SetAITarget()
    {
        if (AIDestinationSetter.target == null)
        {
            AIDestinationSetter.target = _enemyTargetTransformHandler.FindRandomTargetTransform();
        }
    }
}
