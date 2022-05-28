using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private readonly string VOLUME_MIXER_VARIABLE = "volume";
    [SerializeField] private int _startingVolume = 6;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioMixer _mainAudioMixer;

    private void Start() => SetStartingVolume();

    private void SetStartingVolume()
    {
        _volumeSlider.value = _startingVolume;
        ChangeVolume(_startingVolume);
    }

    public void ChangeVolume(System.Single volume) => _mainAudioMixer.SetFloat(VOLUME_MIXER_VARIABLE, volume);
}
