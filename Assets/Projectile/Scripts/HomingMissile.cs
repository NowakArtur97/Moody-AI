using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : BaseProjectile
{
    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private float _rotationVelocity = 200.0f;

    private Rigidbody2D _myRigidbody2D;
    private Transform _target;
    private Vector3 _projectileDirection;
    private float _rotateAmount;
    private AudioSource _flyingAudioSource;

    protected override void Awake()
    {
        base.Awake();

        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _flyingAudioSource = GetComponentsInChildren<AudioSource>()
            .FirstOrDefault(component => component.gameObject != gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _flyingAudioSource.Play();
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            if (_target == null)
            {
                FindClosestEnemy();
            }
            else
            {
                ChaseTarget();
            }
        }
        else
        {
            _myRigidbody2D.angularVelocity = 0;
            _myRigidbody2D.velocity = Vector3.zero;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        _flyingAudioSource.Pause();

        base.OnTriggerEnter2D(collision);
    }

    private void ChaseTarget()
    {
        _projectileDirection = (_target.position - transform.position).normalized;

        _rotateAmount = Vector3.Cross(_projectileDirection, transform.up).z;

        _myRigidbody2D.angularVelocity = -_rotateAmount * _rotationVelocity;

        _myRigidbody2D.velocity = transform.up * MovementVelocity;
    }

    private void FindClosestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        List<Transform> enemies = GameObject.FindGameObjectsWithTag(_enemyTag)
            .Select(gameObject => gameObject.transform)
            .ToList();
        Vector3 directionToTarget;
        float dSqrToTarget;

        foreach (Transform enemy in enemies)
        {
            directionToTarget = enemy.position - currentPosition;
            dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemy;
            }
        }

        _target = bestTarget;
    }
}
