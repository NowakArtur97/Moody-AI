using System.Linq;
using UnityEngine;

public class BodyPartsExplosion : MonoBehaviour
{
    private Animator[] _bodyPartsAnimators;
    private EnemyHealthSystem _enemyHealthSystem;

    private const string EXPLODE_TRIGGER = "explode";

    private void Awake()
    {
        _enemyHealthSystem = transform.parent.parent.GetComponentInChildren<EnemyHealthSystem>();
        _enemyHealthSystem.OnEnemyDeath += ExplodeBodyParts;

        _bodyPartsAnimators = GetComponentsInChildren<Animator>();
    }

    private void OnDestroy() => _enemyHealthSystem.OnEnemyDeath -= ExplodeBodyParts;

    private void ExplodeBodyParts() => _bodyPartsAnimators.ToList().ForEach(bodyPart => bodyPart.SetTrigger(EXPLODE_TRIGGER));
}
