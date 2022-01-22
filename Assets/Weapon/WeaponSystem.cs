using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private int _curentWeaponIndex;
    private int _mouseScrollInput;
    private List<Weapon> _allWeapons;

    public Weapon CurentWeapon { get; private set; }

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _curentWeaponIndex = 0;
        _allWeapons = GetComponentsInChildren<Weapon>().ToList();
        CurentWeapon = _allWeapons[_curentWeaponIndex];

        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
    }

    private void Update()
    {
        _mouseScrollInput = _playerInputManager.MouseScrollInput;

        if (_mouseScrollInput != 0)
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        _curentWeaponIndex += _mouseScrollInput;

        if (_curentWeaponIndex >= _allWeapons.Count)
        {
            _curentWeaponIndex = 0;
        }
        else if (_curentWeaponIndex < 0)
        {
            _curentWeaponIndex = _allWeapons.Count - 1;
        }

        CurentWeapon = _allWeapons[_curentWeaponIndex];
    }
}
