using System.Collections.Generic;
using System.Linq;
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

        _waveManager.OnStartWave += ChoosePlanetsToDefendOnStartOfWave;

        _targetPlanets = new List<Transform>();

        _planetsTargetTransforms.ToList()
            .ForEach(planet => planet.GetComponentInChildren<PlanetHealthSystem>()
            .OnPlanetDestroyed += RemovePlanetToDefend);
    }

    private void OnDestroy() => _waveManager.OnStartWave -= ChoosePlanetsToDefendOnStartOfWave;

    public Transform GetRandomTransform(TargetType targetType)
    {
        switch (targetType)
        {
            case (TargetType.PLAYER):
                return FindPlayerTransform();
            default:
                if (_targetPlanets.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return _targetPlanets[UnityEngine.Random.Range(0, _targetPlanets.Count)];
                }
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

    private void RemovePlanetToDefend(Transform planetTransform)
    {
        planetTransform.GetComponentInChildren<PlanetHealthSystem>().OnPlanetDestroyed -= RemovePlanetToDefend;

        _targetPlanets.Remove(planetTransform);

        Destroy(planetTransform.gameObject);

        if (_planetsTargetTransforms.Count() == 0)
        {
            // TODO: GAME OVER SCREEN
            Debug.Log("GAME OVER");
            return;
        }

        ChoosePlanetToDefend();
    }

    private void ChoosePlanetsToDefendOnStartOfWave(int numberOfWave)
    {
        if (_targetPlanets.Count == (numberOfWave % _numberOfWavesAfterChosingNewPlanetToDefend) + 1
            || _planetsTargetTransforms.Length == 0)
        {
            return;
        }

        ChoosePlanetToDefend();
    }

    private void ChoosePlanetToDefend()
    {
        Transform newRandomTarget = ChoseNewRandomPlanetToDefend();
        _targetPlanets.Add(newRandomTarget);
        _planetsTargetTransforms = _planetsTargetTransforms.Where(planet => planet != newRandomTarget).ToArray();

        SpawnShieldForNewPlanetTarget(newRandomTarget);
    }

    private Transform ChoseNewRandomPlanetToDefend()
    {
        Transform newRandomTarget = _planetsTargetTransforms[UnityEngine.Random.Range(0, _planetsTargetTransforms.Length)];
        return newRandomTarget;
    }

    private void SpawnShieldForNewPlanetTarget(Transform newRandomTarget)
    {
        GameObject shieldPrefab = Instantiate(_shieldPrefab, Vector2.zero, Quaternion.identity);
        shieldPrefab.transform.parent = newRandomTarget;
        shieldPrefab.transform.position = newRandomTarget.position;
    }
}