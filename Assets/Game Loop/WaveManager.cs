using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int _numberOfWave = 0;

    public Action<int> OnStartWave;

    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += IncreaseNumberOfWave;
    }

    private void Start() => IncreaseNumberOfWave();

    private void OnDestroy() => _waveSpawner.OnFinishWave -= IncreaseNumberOfWave;

    private void IncreaseNumberOfWave()
    {
        _numberOfWave++;

        OnStartWave?.Invoke(_numberOfWave);
    }
}
