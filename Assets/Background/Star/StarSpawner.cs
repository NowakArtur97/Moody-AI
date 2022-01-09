using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private int _numberOfStars = 100;

    private void Start() => SpawnStars();

    private void SpawnStars()
    {
        Camera mainCamera = Camera.main;
        int screenHeight = Screen.height;
        int screenWidth = Screen.width;
        Vector2 minPosition = Vector2.zero;
        Vector2 maxXPosition = new Vector2(screenWidth, 0);
        Vector2 maxYPosition = new Vector2(0, screenHeight);
        Vector2 spawnPosition = new Vector2();
        GameObject star;

        for (int i = 0; i < _numberOfStars; i++)
        {
            float spawnX = Random.Range(mainCamera.ScreenToWorldPoint(minPosition).x, mainCamera.ScreenToWorldPoint(maxXPosition).x);
            float spawnY = Random.Range(mainCamera.ScreenToWorldPoint(minPosition).y, mainCamera.ScreenToWorldPoint(maxYPosition).y);

            spawnPosition.Set(spawnX, spawnY);
            star = Instantiate(_starPrefab, spawnPosition, Quaternion.identity);
            star.transform.parent = gameObject.transform;
        }
    }
}
