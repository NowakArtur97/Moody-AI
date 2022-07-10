using Pathfinding;
using System.Collections;
using UnityEngine;

public class RandomPositionTargetToAIPath : TargetToAIPath
{
    [SerializeField] private float _xPosition = 23.0f;
    [SerializeField] private float _yPosition = 11.0f;
    [SerializeField] private float _distanceWhenShouldFindNewPosition = 3.0f;
    [SerializeField] private GameObject _randomTargetGameObject;
    [SerializeField] private float _timeToCheckIfPositionChanged = 0.1f;
    [SerializeField] private float _timeToMoveBack = 0.4f;
    [SerializeField] private float _movementSpeed = 2.0f;

    private AIPath _aiPath;

    private Vector3 _randomPosition;
    private Vector3 _lastPosition;
    private bool _isCheckingPosition;
    private bool _isStuck;
    private bool _isMovingBack;

    protected override void Awake()
    {
        base.Awake();

        _aiPath = GetComponent<AIPath>();
    }

    private void OnEnable()
    {
        _isCheckingPosition = false;
        _isStuck = false;
        _isMovingBack = false;
    }

    protected override void SetAITarget()
    {
        if ((AIDestinationSetter.target == null
            || Vector2.Distance(transform.position, _randomPosition) <= _distanceWhenShouldFindNewPosition)
            && !_isStuck)
        {
            FindRandomPosition();
        }

        if (!_isCheckingPosition)
        {
            StartCoroutine(CheckIfIsMovingCoroutine());
        }

        if (_isStuck && !_isMovingBack)
        {
            StartCoroutine(MoveBackCoroutine());
        }

        if (_isMovingBack)
        {
            MoveBackwards();
        }
    }

    private void FindRandomPosition()
    {
        _randomPosition.Set(Random.Range(-_xPosition, _xPosition), Random.Range(-_yPosition, _yPosition), 0.0f);

        _randomTargetGameObject.transform.position = _randomPosition;

        AIDestinationSetter.target = _randomTargetGameObject.transform;
    }

    private IEnumerator CheckIfIsMovingCoroutine()
    {
        _isCheckingPosition = true;

        _lastPosition = transform.position;

        yield return new WaitForSeconds(_timeToCheckIfPositionChanged);

        if (_lastPosition == transform.position)
        {
            _isStuck = true;
        }

        _isCheckingPosition = false;
    }

    private IEnumerator MoveBackCoroutine()
    {
        _isMovingBack = true;

        _aiPath.canMove = false;

        yield return new WaitForSeconds(_timeToMoveBack);

        _isStuck = false;
        _isMovingBack = false;

        _aiPath.canMove = true;
    }

    private void MoveBackwards() => transform.Translate(Vector2.down * _movementSpeed * Time.deltaTime);
}
