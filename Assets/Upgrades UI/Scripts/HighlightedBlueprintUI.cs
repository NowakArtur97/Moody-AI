using UnityEngine;
using UnityEngine.UI;
using static ProjectileObjectPool;

public class HighlightedBlueprintUI : MonoBehaviour
{
    [SerializeField] private ProjectileType _projectileType;

    private Button _myButton;
    private WeaponUpgradeHandler _weaponUpgradeHandler;

    private void Awake()
    {
        _myButton = GetComponent<Button>();
        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _weaponUpgradeHandler.OnUpdateWeapon += HandleButtonHighlight;
    }

    private void OnDestroy() => _weaponUpgradeHandler.OnUpdateWeapon -= HandleButtonHighlight;

    private void OnEnable() => HandleButtonHighlight();

    private void HandleButtonHighlight()
    {
        if (_projectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType)
        {
            //_myButton.Select();
        }
    }
}
