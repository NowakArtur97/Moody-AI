using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int _numberOfWave = 0;

    public Action<int> OnStartWave;

    private void Awake() => FindObjectOfType<WaveSpawner>().OnFinishWave += IncreaseNumberOfWave;

    private void Start() => IncreaseNumberOfWave();

    private void OnDestroy() => FindObjectOfType<WaveSpawner>().OnFinishWave -= IncreaseNumberOfWave;

    private void IncreaseNumberOfWave()
    {
        _numberOfWave++;

        OnStartWave?.Invoke(_numberOfWave);
    }
}
