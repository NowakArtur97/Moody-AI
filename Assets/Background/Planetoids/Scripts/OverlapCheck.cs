using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{
    [SerializeField] private float _checkRadius = 1.5f;
    [SerializeField] private LayerMask _layersToCheck;

    private void OnEnable()
    {
        List<Collider2D> colliderCheck = FindAllOverlappingObjects();

        if (colliderCheck.Count > 0)
        {
            Destroy(gameObject);
        }
    }

    private List<Collider2D> FindAllOverlappingObjects() =>
        Physics2D.OverlapCircleAll(transform.position, _checkRadius, _layersToCheck)
        .Where(collsion => collsion.gameObject != gameObject)
        .Where(collsion => collsion.transform.parent.gameObject != gameObject)
        .ToList();
}
