using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomTargetTransformManager : MonoBehaviour
{
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private Transform[] _planetsTargetTransforms;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private int _numberOfWavesAfterChosingNewPlanetToDefend = 10;

    private Transform _playerTransform;

    public enum TargetType { PLANET, PLAYER }

    private WaveManager _waveManager;
    private List<Transform> _targetPlanets;

    private void Awake()
    {
        _waveManager = FindObjectOfType<WaveManager>();

        _waveManager.OnStartWave += ChoosePlanetsToDefend;

        _targetPlanets = new List<Transform>();
    }

    private void OnDestroy() => _waveManager.OnStartWave -= ChoosePlanetsToDefend;

    public Transform GetRandomTransform(TargetType targetType)
    {
        switch (targetType)
        {
            case (TargetType.PLAYER):
                return FindPlayerTransform();
            default:
                return _targetPlanets[UnityEngine.Random.Range(0, _targetPlanets.Count)];
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

    private void ChoosePlanetsToDefend(int numberOfWave)
    {
        if (_targetPlanets.Count == (numberOfWave % _numberOfWavesAfterChosingNewPlanetToDefend) + 1
            || _targetPlanets.Count == _planetsTargetTransforms.Length)
        {
            return;
        }

        Transform newRandomTarget = ChoseNewRandomPlanetToDefend();
        _targetPlanets.Add(newRandomTarget);

        SpawnShieldForNewPlanetTarget(newRandomTarget);
    }

    private Transform ChoseNewRandomPlanetToDefend()
    {
        Transform[] notChosenPlanets = Array.FindAll(_planetsTargetTransforms, planet => !_targetPlanets.Contains(planet));
        Transform newRandomTarget = notChosenPlanets[UnityEngine.Random.Range(0, _planetsTargetTransforms.Length)];
        return newRandomTarget;
    }

    private void SpawnShieldForNewPlanetTarget(Transform newRandomTarget)
    {
        GameObject shieldPrefab = Instantiate(_shieldPrefab, Vector2.zero, Quaternion.identity);
        shieldPrefab.transform.parent = newRandomTarget;
        shieldPrefab.transform.position = newRandomTarget.position;
    }
}