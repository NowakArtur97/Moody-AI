using UnityEngine;

public class RandomPositionTargetToAIPath : TargetToAIPath
{
    [SerializeField] private float _xPosition = 23.0f;
    [SerializeField] private float _yPosition = 11.0f;
    [SerializeField] private float _distanceWhenShouldFindNewPosition = 3.0f;
    [SerializeField] private GameObject _randomTargetGameObject;

    private Vector3 _randomPosition;

    protected override void SetAITarget()
    {
        if (AIDestinationSetter.target == null
            || Vector2.Distance(transform.position, _randomPosition) <= _distanceWhenShouldFindNewPosition)
        {
            FindRandomPosition();

            AIDestinationSetter.target = _randomTargetGameObject.transform;
        }
    }

    private void FindRandomPosition()
    {
        _randomPosition.Set(Random.Range(-_xPosition, _xPosition), Random.Range(-_yPosition, _yPosition), 0.0f);

        _randomTargetGameObject.transform.position = _randomPosition;
    }
}
