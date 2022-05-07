using TMPro;
using UnityEngine;

public class ChangeViewButtonText : MonoBehaviour
{
    private readonly string WEAPON_SELECTION_MENU_BUTTON_TEXT = "Enter";
    private readonly string WEAPON_UPGRADE_MENU_BUTTON_TEXT = "Exit";

    private TMP_Text _unlockButtonText;
    private bool _isInWeaponSelectionMenu;

    private void Awake()
    {
        _unlockButtonText = GetComponentInChildren<TMP_Text>();

        _isInWeaponSelectionMenu = true;

        _unlockButtonText.text = WEAPON_SELECTION_MENU_BUTTON_TEXT;
    }

    public void ChangeText()
    {
        _isInWeaponSelectionMenu = !_isInWeaponSelectionMenu;
        _unlockButtonText.text = _isInWeaponSelectionMenu ? WEAPON_SELECTION_MENU_BUTTON_TEXT : WEAPON_UPGRADE_MENU_BUTTON_TEXT;
    }
}
