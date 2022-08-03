using UnityEngine;

public class PlanetoidSpawner : MonoBehaviour
{
    [SerializeField] private float _minRadius = 8f;
    [SerializeField] private float _maxRadius = 13f;
    [SerializeField] private int _numberOfPlanetoids = 100;
    [SerializeField] private Vector2 _origin = Vector2.one;
    [SerializeField] private GameObject[] _planetoids;

    private void Start() => SpawnPlanetoids();

    private void SpawnPlanetoids()
    {
        for (int i = 0; i < _numberOfPlanetoids; i++)
        {
            Vector2 pointToSpawnAt = RandomPointInAnnulus();

            GameObject planetoid = Instantiate(ChoseRandomPlanetoid(), pointToSpawnAt, Quaternion.identity);

            planetoid.transform.parent = gameObject.transform;
        }
    }

    private GameObject ChoseRandomPlanetoid() => _planetoids[Random.Range(0, _planetoids.Length)];

    private Vector2 RandomPointInAnnulus()
    {
        Vector2 randomDirection = (Random.insideUnitCircle * _origin).normalized;

        float randomDistance = Random.Range(_minRadius, _maxRadius);

        return _origin + randomDirection * randomDistance;
    }
}
