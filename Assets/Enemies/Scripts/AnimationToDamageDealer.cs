using UnityEngine;

public class AnimationToDamageDealer : MonoBehaviour
{
    private DamageDealer _damageDealer;

    private void Awake() => _damageDealer = gameObject.transform.parent.GetComponentInChildren<DamageDealer>();

    public void DamageTrigger() => _damageDealer.DamageTrigger();
}
