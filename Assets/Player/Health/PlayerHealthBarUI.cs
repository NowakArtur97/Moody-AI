using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";

    private Scrollbar _myScrollbar;
    private HealthSystem _playerHealthSystem;

    private void Awake() => _myScrollbar = GetComponent<Scrollbar>();

    private void Update()
    {
        if (_playerHealthSystem == null)
        {
            _playerHealthSystem = GameObject.FindWithTag(PLAYER_TAG).GetComponentInChildren<HealthSystem>();
        }

        _myScrollbar.size = _playerHealthSystem.CurrentHealth / _playerHealthSystem.MaxHealth;
    }
}
