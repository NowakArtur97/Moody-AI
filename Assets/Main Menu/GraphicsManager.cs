using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionsDropdown;

    private Resolution[] _availableResolutions;

    private void Awake()
    {
        SetupResolutions();
        ChoseDefaultOptions();
    }

    private void SetupResolutions()
    {
        _availableResolutions = Screen.resolutions;

        _resolutionsDropdown.ClearOptions();

        _resolutionsDropdown.AddOptions(
          _availableResolutions.Select(resolution => $"{resolution.width} x {resolution.height}")
          .Distinct()
          .ToList());
    }

    private void ChoseDefaultOptions()
    {
        int currentResolutionIndex = Array.FindIndex(_availableResolutions, resolution => HasSameResolution(resolution));
        _resolutionsDropdown.value = currentResolutionIndex;
        _resolutionsDropdown.RefreshShownValue();

        SetResolution(currentResolutionIndex);
        SetFullScreen(true);
    }

    private bool HasSameResolution(Resolution resolution) => resolution.width == Screen.currentResolution.width
             && resolution.height == Screen.currentResolution.height;

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;
}
