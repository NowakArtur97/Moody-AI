using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AmmoRestorationManager;
using static ProjectileObjectPool;

public class WeaponAmmoBarImageUI : MonoBehaviour
{
    [SerializeField] private AmmoRestorationType _ammoRestorationType;
    public AmmoRestorationType RestorationType
    {
        get { return _ammoRestorationType; }
        set { _ammoRestorationType = value; }
    }
    [SerializeField] private Sprite _defaultWeaponSprite;
    [SerializeField] private Sprite _alienWeaponSprite;
    [SerializeField] private Sprite _rocketLauncherSprite;
    [SerializeField] private Sprite _mineThrowerSprite;
    [SerializeField] private Sprite _shurikenThrowerSprite;

    private Image _myImage;
    private List<WeaponAmmoConsumptionManager> _weaponAmmoConsumptionManager;

    private void Awake()
    {
        _myImage = GetComponent<Image>();

        _weaponAmmoConsumptionManager = FindObjectsOfType<WeaponAmmoConsumptionManager>().ToList();
    }

    private void OnEnable() => SetupImage();

    private void SetupImage()
    {
        WeaponAmmoConsumptionManager weaponAmmoConsumptionManager = _weaponAmmoConsumptionManager
            .Find(manager => manager.RestorationType == _ammoRestorationType);

        if (weaponAmmoConsumptionManager != null)
        {
            switch (weaponAmmoConsumptionManager.ProjectileType)
            {
                case ProjectileType.DEFAULT_BULLET:
                    _myImage.sprite = _defaultWeaponSprite;
                    break;
                case ProjectileType.BALL_PROJECTILE:
                    _myImage.sprite = _alienWeaponSprite;
                    break;
                case ProjectileType.HOMING_MISSILE:
                    _myImage.sprite = _rocketLauncherSprite;
                    break;
                case ProjectileType.SPIKE_MINE:
                    _myImage.sprite = _mineThrowerSprite;
                    break;
                case ProjectileType.SHURIKEN:
                    _myImage.sprite = _shurikenThrowerSprite;
                    break;
            }
        }
    }
}
