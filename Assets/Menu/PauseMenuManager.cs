using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _menuCanvas;

    private PlayerInputManager _playerInputManager;
    private WaveNumberUI _waveNumberUI;

    private void Start()
    {
        _playerInputManager = FindObjectOfType<PlayerInputManager>();
        _waveNumberUI = FindObjectOfType<WaveNumberUI>();
    }

    private void Update()
    {
        if (_playerInputManager == null)
        {
            _playerInputManager = FindObjectOfType<PlayerInputManager>();
        }
        else if (_playerInputManager.PauseInput && Time.timeScale != 0)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        SetValues(true);

        if (_waveNumberUI.IsAnimating)
        {
            StopCoroutine(_waveNumberUI.AnimateTextCoroutine());
        }
    }

    public void ResumeGame()
    {
        SetValues(false);

        if (_waveNumberUI.IsAnimating)
        {
            StartCoroutine(_waveNumberUI.AnimateTextCoroutine());
        }
    }

    private void SetValues(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
        _gameCanvas.SetActive(!isPaused);
        _menuCanvas.SetActive(isPaused);
    }
}
