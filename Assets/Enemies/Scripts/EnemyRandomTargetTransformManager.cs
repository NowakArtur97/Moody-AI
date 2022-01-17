using UnityEngine;
public class EnemyRandomTargetTransformManager : MonoBehaviour
{
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private Transform[] _targetTransforms;
    private Transform _playerTransform;

    public enum TargetType { PLANET, PLAYER }

    // TODO: RandomTargetTransformEnemyManager: Add more targets with every ten waves
    public Transform GetRandomTransform(TargetType targetType)
    {
        switch (targetType)
        {
            case (TargetType.PLAYER):
                return FindPlayerTransform();
            default:
                return _targetTransforms[0];
        }
    }

    private Transform FindPlayerTransform()
    {
        if (_playerTransform == null)
        {
            _playerTransform = GameObject.FindGameObjectWithTag(_playerTag).transform;
        }

        return _playerTransform;
    }
}