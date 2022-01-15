using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int _numberOfWave = 1;

    public Action<int> OnStartSpawning;

    private void Start() => OnStartSpawning?.Invoke(_numberOfWave);

    private void IncreaseNumberOfWave() => _numberOfWave++;
}
