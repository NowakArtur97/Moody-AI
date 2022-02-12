using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AmmoRestorationManager;
using static ProjectileObjectPool;

public class WeaponMoodUI : MonoBehaviour
{
    [SerializeField] ProjectileType _projectileType;
    [SerializeField] Sprite _emptyFaceSprite;
    [SerializeField] Sprite _happyFaceSprite;
    [SerializeField] Sprite _angryFaceSprite;
    [SerializeField] Sprite _sadFaceSprite;
    [SerializeField] Sprite _sleepyFaceSprite;
    [SerializeField] Sprite _surprisedFaceSprite;

    private Image _myImage;
    private WeaponUpgradeManager _weaponUpgradeManager;
    private WeaponUpgradeHandler _weaponUpgradeHandler;
    private WeaponAmmoConsumptionHandler _weaponAmmoConsumptionHandler;
    private WeaponAmmoConsumptionManager _weaponAmmoConsumptionManager;
    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _myImage = GetComponent<Image>();

        _weaponUpgradeHandler = FindObjectOfType<WeaponUpgradeHandler>();
        _weaponUpgradeHandler.OnUpdateWeapon += ChangeWeaponMoodSprite;
        _weaponAmmoConsumptionHandler = FindObjectOfType<WeaponAmmoConsumptionHandler>();
        _weaponAmmoConsumptionHandler.OnChangeRestorationType += ChangeWeaponMoodSprite;
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        _waveSpawner.OnFinishWave += ChangeWeaponMoodSprite;
    }

    private void OnDestroy()
    {
        _weaponUpgradeHandler.OnUpdateWeapon -= ChangeWeaponMoodSprite;
        _weaponAmmoConsumptionHandler.OnChangeRestorationType -= ChangeWeaponMoodSprite;
        _waveSpawner.OnFinishWave -= ChangeWeaponMoodSprite;
    }

    private void ChangeWeaponMoodSprite()
    {
        if (_weaponUpgradeManager == null)
        {
            _weaponUpgradeManager = FindObjectsOfType<WeaponUpgradeManager>()
                .ToList()
                .Find(manager => manager.ProjectileType == _projectileType);
        }
        if (_weaponAmmoConsumptionManager == null)
        {
            _weaponAmmoConsumptionManager = FindObjectsOfType<WeaponAmmoConsumptionManager>()
                .ToList()
                 .Find(manager => manager.ProjectileType == _projectileType);
        }

        bool isUnlocked = _weaponUpgradeManager.IsUnlocked;

        if (isUnlocked)
        {
            switch (_weaponAmmoConsumptionManager.RestorationType)
            {
                case AmmoRestorationType.DEFEATING_ENEMIES:
                    _myImage.sprite = _happyFaceSprite;
                    break;
                case AmmoRestorationType.TAKING_DAMAGE:
                    _myImage.sprite = _angryFaceSprite;
                    break;
                case AmmoRestorationType.DAMAGING_PLANET:
                    _myImage.sprite = _sadFaceSprite;
                    break;
                case AmmoRestorationType.STAYING_IN_PLACE:
                    _myImage.sprite = _sleepyFaceSprite;
                    break;
                case AmmoRestorationType.DODGING_BULLETS:
                    _myImage.sprite = _surprisedFaceSprite;
                    break;
            }
        }
        else
        {
            _myImage.sprite = _emptyFaceSprite;
        }
    }
}
