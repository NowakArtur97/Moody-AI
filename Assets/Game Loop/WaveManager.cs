using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int _numberOfWave = 0;

    public Action<int> OnStartWave;

    private UpgradesCanvasUI _upgradesCanvasUI;

    private void Awake()
    {
        _upgradesCanvasUI = FindObjectOfType<UpgradesCanvasUI>();
        _upgradesCanvasUI.OnCloseUI += IncreaseNumberOfWave;
    }

    private void Start() => IncreaseNumberOfWave();

    private void OnDestroy() => _upgradesCanvasUI.OnCloseUI -= IncreaseNumberOfWave;

    private void IncreaseNumberOfWave()
    {
        _numberOfWave++;

        OnStartWave?.Invoke(_numberOfWave);
    }
}
