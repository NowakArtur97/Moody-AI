using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Tentacle : MonoBehaviour
{
    [SerializeField] private int _numberOfSegments = 30;
    [SerializeField] private Transform _tentacleTargetRotation;
    [SerializeField] private float _targetDistance = 0.2f;
    [SerializeField] private float _timeToReachTarget = 0.02f;
    [SerializeField] private float _trailSpeed = 350f;

    [SerializeField] private float _wigglingSpeed = 10.0f;
    [SerializeField] private float _wigglingMagnitude = 20.0f;
    [SerializeField] private Transform _wigglingDirection;

    private LineRenderer _myLineRenderer;
    private Vector3[] _segmentsPositions;
    private Vector3[] _segmentsVelocitites;

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

        for (int i = 1; i < _numberOfSegments; i++)
        {
            _segmentsPositions[i] = Vector3.SmoothDamp(_segmentsPositions[i],
                _segmentsPositions[i - 1] + _tentacleTargetRotation.right * _targetDistance,
                ref _segmentsVelocitites[i], _timeToReachTarget + i / _trailSpeed);
        }
        _myLineRenderer.SetPositions(_segmentsPositions);
    }
}
