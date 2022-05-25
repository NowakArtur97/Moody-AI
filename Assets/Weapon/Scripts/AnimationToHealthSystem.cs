using UnityEngine;

public class AnimationToHealthSystem : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private void Awake() => _healthSystem = transform.parent.GetComponentInChildren<HealthSystem>();

    public void DeathTrigger() => _healthSystem.DeathTrigger();
}