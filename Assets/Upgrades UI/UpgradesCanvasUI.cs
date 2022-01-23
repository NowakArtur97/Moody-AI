using System;
using UnityEngine;

public class UpgradesCanvasUI : MonoBehaviour
{
    public Action OnCloseUI;

    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += OpenUpgradesUI;
    }

    private void Start() => gameObject.SetActive(false);

    private void OnDestroy() => _waveSpawner.OnFinishWave -= OpenUpgradesUI;

    private void OpenUpgradesUI() { gameObject.SetActive(true); Debug.Log("HALO"); }

    public void CloseUpgradesUI()
    {
        gameObject.SetActive(false);
        OnCloseUI?.Invoke();
    }
}
