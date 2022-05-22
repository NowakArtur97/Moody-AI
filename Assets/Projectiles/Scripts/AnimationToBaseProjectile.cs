using UnityEngine;

public class AnimationToBaseProjectile : MonoBehaviour
{
    public void ReleaseTrigger() => GetComponentInParent<BaseProjectile>().ReleaseTrigger();
}
