using Pathfinding;
using UnityEngine;

public abstract class TargetToAIPath : MonoBehaviour
{
    protected AIDestinationSetter AIDestinationSetter { get; private set; }

    protected virtual void Awake() => AIDestinationSetter = GetComponent<AIDestinationSetter>();

    private void Update() => SetAITarget();

    protected abstract void SetAITarget();
}
