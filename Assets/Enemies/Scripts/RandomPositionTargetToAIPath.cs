using UnityEngine;

public class RandomPositionTargetToAIPath : TargetToAIPath
{
    [SerializeField] private float _distanceWhenShouldFindNewPosition = 3.0f;
    [SerializeField] private GameObject _randomTargetGameObject;

    private Vector3 _randomPosition;
    private int _screenWidth;
    private int _screenHeight;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
    }

    protected override void SetAITarget()
    {
        if (AIDestinationSetter.target == null
            || Vector2.Distance(transform.position, _randomPosition) <= _distanceWhenShouldFindNewPosition)
        {
            FindRandomPositionInCameraBounds();

            _randomTargetGameObject.transform.position = _randomPosition;

            AIDestinationSetter.target = _randomTargetGameObject.transform;
        }
    }

    private void FindRandomPositionInCameraBounds()
    {
        _randomPosition.Set(Random.Range(0, _screenWidth), Random.Range(0, _screenHeight), 0.0f);
        _randomPosition = _mainCamera.ScreenToWorldPoint(_randomPosition);
    }
}
