using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Tentacle : MonoBehaviour
{
    [SerializeField] private TentacleMode _tentacleMode;

    [SerializeField] private int _numberOfSegments = 30;
    [SerializeField] private Transform _tentacleTargetRotation;
    [SerializeField] private float _targetDistance = 0.2f;
    [SerializeField] private float _lastTargetDistance = 0.4f;
    [SerializeField] private float _timeToReachTarget = 0.02f;
    [SerializeField] private float _trailSpeed = 350f;

    [SerializeField] private float _wigglingSpeed = 10.0f;
    [SerializeField] private float _wigglingMagnitude = 20.0f;
    [SerializeField] private Transform _wigglingDirection;

    [SerializeField] private Transform[] _bodyParts;
    [SerializeField] private Transform _tail;

    private LineRenderer _myLineRenderer;
    private Vector3[] _segmentsPositions;
    private Vector3[] _segmentsVelocitites;

    private enum TentacleMode { TENTACLE, BODY_PART }

    private void Awake()
    {
        _myLineRenderer = GetComponent<LineRenderer>();
        _myLineRenderer.positionCount = _numberOfSegments;
        _segmentsPositions = new Vector3[_numberOfSegments];
        _segmentsVelocitites = new Vector3[_numberOfSegments];

        SetupSegments();
    }

    private void Update() => SetupSegments();

    private void SetupSegments()
    {
        _wigglingDirection.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * _wigglingSpeed) * _wigglingMagnitude);

        _segmentsPositions[0] = _tentacleTargetRotation.position;

        for (int index = 1; index < _numberOfSegments; index++)
        {
            switch (_tentacleMode)
            {
                case TentacleMode.TENTACLE:
                    _segmentsPositions[index] = SetSegmentPosition(index, _segmentsPositions[index - 1] + _tentacleTargetRotation.right * _targetDistance,
                         _timeToReachTarget + index / _trailSpeed);
                    break;

                case TentacleMode.BODY_PART:
                    if (index == _numberOfSegments - 1)
                    {
                        _segmentsPositions[index] = SetSegmentPosition(index, _segmentsPositions[index - 1] + (_segmentsPositions[index] - _segmentsPositions[index - 1]).normalized * _lastTargetDistance, _timeToReachTarget);
                    }
                    else
                    {
                        _segmentsPositions[index] = SetSegmentPosition(index, _segmentsPositions[index - 1] + (_segmentsPositions[index] - _segmentsPositions[index - 1]).normalized * _targetDistance, _timeToReachTarget);
                    }

                    _bodyParts[index - 1].transform.position = _segmentsPositions[index];
                    break;
            }
        }
        _myLineRenderer.SetPositions(_segmentsPositions);

        if (_tail)
        {
            _tail.position = _segmentsPositions[_numberOfSegments - 1];
        }
    }

    private Vector3 SetSegmentPosition(int index, Vector3 targetPosition, float smoothTime) =>
        Vector3.SmoothDamp(_segmentsPositions[index], targetPosition, ref _segmentsVelocitites[index], smoothTime);
}
