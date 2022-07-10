using UnityEngine;

public class BodyPartColliderManager : MonoBehaviour
{
    private readonly string EMPTY_LAYER = "Empty";

    [SerializeField] private EnemyHealthSystem _enemyHealthSystem;

    private Collider2D _myCollider2D;

    private int _defaultLayer;

    private void Awake()
    {
        _enemyHealthSystem.OnEnemyDeath += DisableCollider;

        _myCollider2D = GetComponent<Collider2D>();

        _defaultLayer = gameObject.layer;
    }

    private void OnEnable() => gameObject.layer = _defaultLayer;

    private void OnDestroy() => _enemyHealthSystem.OnEnemyDeath -= DisableCollider;

    private void DisableCollider() => gameObject.layer = LayerMask.NameToLayer(EMPTY_LAYER);
}
