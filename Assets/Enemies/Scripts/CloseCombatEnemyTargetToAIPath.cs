using Pathfinding;
using UnityEngine;

public class CloseCombatEnemyTargetToAIPath : TargetToAIPath
{
    [SerializeField] private float _distanceToIgnorePlanet = 1.4f;
    [SerializeField] private float _distanceToAgainAttackPlanet = 0.1f;
    [SerializeField] private float _randomTargetRadius = 4f;
    [SerializeField] private float _movementSpeed = 6f;

    private AIPath _aiPath;
    private bool _isMovingTowardsRandomTarget;
    private Vector2 _randomPosition;

    protected override void Awake()
    {
        base.Awake();

        _aiPath = GetComponent<AIPath>();

        _isMovingTowardsRandomTarget = false;
    }

    protected override void SetAITarget()
    {
        if (!_isMovingTowardsRandomTarget && AIDestinationSetter.target == null)
        {
            _aiPath.canMove = true;

            AIDestinationSetter.target = EnemyTargetTransformHandler.FindRandomTargetTransform();
        }
        else if (!_isMovingTowardsRandomTarget
            && Vector2.Distance(transform.position, AIDestinationSetter.target.position) < _distanceToIgnorePlanet)
        {
            _isMovingTowardsRandomTarget = true;

            _aiPath.canMove = false;

            _randomPosition = FindRandomPointInRadius();
        }
        else if (_isMovingTowardsRandomTarget
             && Vector2.Distance(transform.position, _randomPosition) > _distanceToAgainAttackPlanet)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _randomPosition,
                _movementSpeed * Time.deltaTime);
        }
        else if (_isMovingTowardsRandomTarget
            && Vector2.Distance(transform.position, _randomPosition) < _distanceToAgainAttackPlanet)
        {
            _isMovingTowardsRandomTarget = false;

            _aiPath.canMove = true;

            AIDestinationSetter.target = EnemyTargetTransformHandler.FindRandomTargetTransform();
        }
    }

    private Vector2 FindRandomPointInRadius()
    {
        Vector2 randomDirection = (Random.insideUnitCircle * transform.position).normalized;

        return (Vector2)transform.position + randomDirection * _randomTargetRadius;
    }
}
