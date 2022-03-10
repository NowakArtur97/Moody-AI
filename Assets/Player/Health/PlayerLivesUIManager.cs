using UnityEngine;

public class PlayerLivesUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _livesWrapper;

    private WaveManager _waveManager;
    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.OnStartWave += ShowLives;

        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += HideLives;
    }

    private void OnDestroy()
    {
        _waveManager.OnStartWave -= ShowLives;
        _waveSpawner.OnFinishWave -= HideLives;
    }

    private void ShowLives(int numberOfWave) => _livesWrapper.SetActive(true);

    private void HideLives() => _livesWrapper.SetActive(false);
}
