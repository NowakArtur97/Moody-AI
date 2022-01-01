using UnityEngine;

public class GettingCloserToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _movementVelocity = 10.0f;
    [SerializeField] private float _distanceWhenShouldStop = 3.0f;

    private void Update()
    {
        if (Vector2.Distance(transform.position, _target.position) > _distanceWhenShouldStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _movementVelocity * Time.deltaTime);
        }
    }
}
