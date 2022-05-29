using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionsDropdown;

    private Resolution[] _availableResolutions;

    private void Start()
    {
        SetupResolutions();
        ChoseDefaultResolution();
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

    private void ChoseDefaultResolution()
    {
        int lastResolutionIndex = _availableResolutions.Length - 1;
        _resolutionsDropdown.value = lastResolutionIndex;
        _resolutionsDropdown.RefreshShownValue();

        SetResolution(_availableResolutions.Length - 1);
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
