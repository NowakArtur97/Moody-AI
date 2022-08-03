using Pathfinding;
using UnityEngine;

public class CloseCombatEnemyTargetToAIPath : TargetToAIPath
{
    [SerializeField] private float _distanceToIgnorePlanet = 1.5f;
    [SerializeField] private float _distanceToAgainAttackPlanet = 0.1f;
    [SerializeField] private float _randomTargetRadius = 4f;
    [SerializeField] private float _movementSpeed = 6f;

    private AIPath _aiPath;
    private RotationController _rotationController;
    private EnemyTargetTransformHandler _enemyTargetTransformHandler;

    private bool _isMovingTowardsRandomTarget;
    private Vector2 _randomPosition;

    protected override void Awake()
    {
        base.Awake();

        _aiPath = GetComponent<AIPath>();
        _rotationController = GetComponent<RotationController>();
        _enemyTargetTransformHandler = GetComponent<EnemyTargetTransformHandler>();

        _isMovingTowardsRandomTarget = false;
    }

    private void OnEnable() => _isMovingTowardsRandomTarget = false;

    protected override void SetAITarget()
    {
        if (_isMovingTowardsRandomTarget)
        {
            if (Vector2.Distance(transform.position, _randomPosition) > _distanceToAgainAttackPlanet)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _randomPosition,
                    _movementSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, _randomPosition) < _distanceToAgainAttackPlanet)
            {
                _isMovingTowardsRandomTarget = false;

                _aiPath.canMove = true;

                AIDestinationSetter.target = _enemyTargetTransformHandler.FindRandomTargetTransform();
            }

            _rotationController.SetTarget(_randomPosition);
        }
        else
        {
            if (AIDestinationSetter.target == null)
            {
                _aiPath.canMove = true;

                AIDestinationSetter.target = _enemyTargetTransformHandler.FindRandomTargetTransform();
            }
            else if (Vector2.Distance(transform.position, AIDestinationSetter.target.position) < _distanceToIgnorePlanet)
            {
                _isMovingTowardsRandomTarget = true;

                _aiPath.canMove = false;

                _randomPosition = FindRandomPointInRadius();
            }

            if (AIDestinationSetter.target != null)
            {
                _rotationController.SetTarget(AIDestinationSetter.target.position);
            }
        }
    }

    private Vector2 FindRandomPointInRadius()
    {
        Vector2 randomDirection = (Random.insideUnitCircle * transform.position).normalized;

        return (Vector2)transform.position + randomDirection * _randomTargetRadius;
    }
}
