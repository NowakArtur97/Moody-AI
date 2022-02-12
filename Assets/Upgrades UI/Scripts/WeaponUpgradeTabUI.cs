using UnityEngine;

public class WeaponUpgradeTabUI : MonoBehaviour
{
    [SerializeField] GameObject _weaponSelectionMenu;
    [SerializeField] GameObject _weaponUpgradeMenu;
    [SerializeField] GameObject _changeViewButton;

    private WeaponUpgradeHandler _weaponUpgradeHandler;

    private void Awake() => _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();

    public void HandleUITab()
    {
        if (_weaponUpgradeHandler.CurrentWeaponUpgradeManager.IsUnlocked)
        {
            _weaponSelectionMenu.SetActive(!_weaponSelectionMenu.activeInHierarchy);
            _weaponUpgradeMenu.SetActive(!_weaponUpgradeMenu.activeInHierarchy);
        }
    }

    public void HandleEnterButton() => _changeViewButton.SetActive(_weaponUpgradeHandler.CurrentWeaponUpgradeManager.IsUnlocked);
}
