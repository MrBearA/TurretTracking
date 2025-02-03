using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum MovementType { Quadratic, Cubic }
    private Transform target;
    private float t;
    private MovementType movementType;
    private EnemySpawner spawner;
    private Vector3 startPos;

    public float speed = 2f;

    public void Init(Transform targetLocation, MovementType type, EnemySpawner spawnerRef)
    {
        target = targetLocation;
        movementType = type;
        spawner = spawnerRef;
        startPos = transform.position;
    }

    void Update()
    {
        t += Time.deltaTime * speed / Vector3.Distance(startPos, target.position);

        if (movementType == MovementType.Quadratic)
        {
            transform.position = Vector3.Lerp(startPos, target.position, t * t);
        }
        else if (movementType == MovementType.Cubic)
        {
            transform.position = Vector3.Lerp(startPos, target.position, t * t * t);
        }

        if (t >= 1f)
        {
            spawner.TakeDamage();
            Destroy(gameObject);
        }
    }
}
