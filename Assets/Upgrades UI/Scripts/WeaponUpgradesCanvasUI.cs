using System;
using UnityEngine;

public class WeaponUpgradesCanvasUI : MonoBehaviour
{
    public Action OnCloseUI;

    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += OpenUI;
    }

    private void Start() => gameObject.SetActive(false);

    private void OnDestroy() => _waveSpawner.OnFinishWave -= OpenUI;

    private void OpenUI() => gameObject.SetActive(true);

    public void CloseUI()
    {
        gameObject.SetActive(false);
        OnCloseUI?.Invoke();
    }
}
