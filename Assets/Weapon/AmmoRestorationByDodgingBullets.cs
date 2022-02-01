using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AmmoRestorationManager;

public class AmmoRestorationByDodgingBullets : MonoBehaviour
{
    [SerializeField] private float _maxDistanceToProjectiles = 2f;
    [SerializeField] private float _timeToWaitBeforeRestoring = 0.5f;
    [SerializeField] private LayerMask _projectilesLayer;

    private bool _shouldRestore;

    private void Awake() => _shouldRestore = true;

    private void Update()
    {
        if (_shouldRestore)
        {
            StartCoroutine(RestoreAmmunitonByDodgingBulletsCoroutine());
        }
    }

    private IEnumerator RestoreAmmunitonByDodgingBulletsCoroutine()
    {
        _shouldRestore = false;

        List<Collider2D> detectedProjectiles = Physics2D.OverlapCircleAll(transform.position, _maxDistanceToProjectiles, _projectilesLayer)
            .ToList();

        yield return new WaitForSeconds(_timeToWaitBeforeRestoring);

        int detectedProjectilesCount = detectedProjectiles
            .Select(projectile => projectile.gameObject.activeInHierarchy)
            .Count();

        for (int i = 0; i < detectedProjectilesCount; i++)
        {
            AmmoRestorationManagerInstance.RestoreAmmunition(AmmoRestorationType.DODGING_BULLETS);
        }

        _shouldRestore = true;
    }
}
