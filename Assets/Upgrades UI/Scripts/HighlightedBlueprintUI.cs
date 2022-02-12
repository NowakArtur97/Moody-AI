using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    private void Update()
    {
        if (_projectileType == _weaponUpgradeHandler.CurrentWeaponUpgradeManager.ProjectileType)
        {
            _myButton.Select();
        }
    }
}
