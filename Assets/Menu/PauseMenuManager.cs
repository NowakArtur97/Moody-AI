using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _menuCanvas;

    private PlayerInputManager _playerInputManager;

    private void Start()
    {
        _playerInputManager = FindObjectOfType<PlayerInputManager>();
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
        Time.timeScale = 0;
        _gameCanvas.SetActive(false);
        _menuCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _gameCanvas.SetActive(true);
        _menuCanvas.SetActive(false);
    }
}
