using UnityEngine;

public class PlayerHealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject _healthBar;

    private WaveManager _waveManager;
    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += ShowBar;

        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += HideBar;
    }

    private void OnDestroy()
    {
        _waveManager.OnStartWave -= ShowBar;
        _waveSpawner.OnFinishWave -= HideBar;
    }

    private void ShowBar(int waveNumber) => _healthBar.SetActive(true);

    private void HideBar() => _healthBar.SetActive(false);
}
