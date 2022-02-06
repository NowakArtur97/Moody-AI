using UnityEngine;

public class WeaponUpgradeTabUI : MonoBehaviour
{
    [SerializeField] GameObject _weaponSelectionMenu;
    [SerializeField] GameObject _weaponUpgradeMenu;

    public void HandleUI()
    {
        _weaponSelectionMenu.SetActive(!_weaponSelectionMenu.activeInHierarchy);
        _weaponUpgradeMenu.SetActive(!_weaponUpgradeMenu.activeInHierarchy);
    }
}
