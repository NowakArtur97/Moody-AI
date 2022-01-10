using UnityEngine;

public class InCameraRangeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private int _numberOfPrefabs = 100;

    private void Start() => SpawnPrefabs();

    private void SpawnPrefabs()
    {
        Camera mainCamera = Camera.main;
        int screenHeight = Screen.height;
        int screenWidth = Screen.width;
        Vector2 minPosition = Vector2.zero;
        Vector2 maxXPosition = new Vector2(screenWidth, 0);
        Vector2 maxYPosition = new Vector2(0, screenHeight);
        Vector2 spawnPosition = new Vector2();
        GameObject instance;

        for (int i = 0; i < _numberOfPrefabs; i++)
        {
            float spawnX = Random.Range(mainCamera.ScreenToWorldPoint(minPosition).x, mainCamera.ScreenToWorldPoint(maxXPosition).x);
            float spawnY = Random.Range(mainCamera.ScreenToWorldPoint(minPosition).y, mainCamera.ScreenToWorldPoint(maxYPosition).y);

            spawnPosition.Set(spawnX, spawnY);
            instance = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], spawnPosition, Quaternion.identity);
            instance.transform.parent = gameObject.transform;
        }
    }
}
