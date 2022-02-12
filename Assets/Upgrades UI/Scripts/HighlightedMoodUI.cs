using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AmmoRestorationManager;

public class HighlightedMoodUI : MonoBehaviour
{
    [SerializeField] private AmmoRestorationType _ammoRestorationType;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _highlightedSprite;

    private Image _myImage;
    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private WeaponAmmoConsumptionHandler _weaponAmmoConsumptionHandler;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManagers;

    private void Awake()
    {
        _myImage = GetComponent<Image>();

        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _weaponUpgradeHandler.OnUpdateWeapon += HandleMoodHighlight;
        _weaponAmmoConsumptionHandler = FindObjectOfType<WeaponAmmoConsumptionHandler>();
        _weaponAmmoConsumptionHandler.OnChangeRestorationType += HandleMoodHighlight;

        _weaponAmmoConsumptionManagers = FindObjectsOfType<WeaponAmmoConsumptionManager>()
              .ToList();
    }

    private void OnDestroy()
    {
        _weaponUpgradeHandler.OnUpdateWeapon -= HandleMoodHighlight;
        _weaponAmmoConsumptionHandler.OnChangeRestorationType -= HandleMoodHighlight;
    }

    private void OnEnable() => HandleMoodHighlight();

    private void HandleMoodHighlight()
    {
        WeaponAmmoConsumptionManager weaponAmmoConsumptionManager = _weaponAmmoConsumptionManagers
            .Find(manager => manager.ProjectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType);

        _myImage.sprite = _ammoRestorationType == weaponAmmoConsumptionManager.RestorationType ? _highlightedSprite : _defaultSprite;
    }
}
