using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private int _curentWeaponIndex;
    private List<Weapon> _allWeapons;

    public Weapon CurentWeapon { get; private set; }

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _allWeapons = GetComponentsInChildren<Weapon>().ToList();
        _curentWeaponIndex = 0;
        Debug.Log(_allWeapons.Count);
        CurentWeapon = _allWeapons[0];

        _playerInputManager = transform.parent.GetComponentInChildren<PlayerInputManager>();
    }

    private void Update()
    {
        Debug.Log(_playerInputManager.MouseScrollInput);
    }
}
