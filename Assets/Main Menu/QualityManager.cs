using TMPro;
using UnityEngine;

public class QualityManager : MonoBehaviour
{
    private const int MAXIMUM_QUALITY_LEVEL = 2;

    [SerializeField] private TMP_Dropdown _graphicsDropdown;

    private void Awake() => ChoseDefaultQuality();

    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    private void ChoseDefaultQuality()
    {
        QualitySettings.SetQualityLevel(MAXIMUM_QUALITY_LEVEL);

        _graphicsDropdown.value = MAXIMUM_QUALITY_LEVEL;
        _graphicsDropdown.RefreshShownValue();
    }
}
