using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f; // Speed of the projectile
    public float rotateSpeed = 200f; // How fast the projectile turns
    public float lifetime = 5f; // How long before it self-destructs

    private Transform target; // The enemy target

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy if it doesn't hit anything
        FindTarget(); // Look for an enemy when spawned
    }

    void Update()
    {
        if (target == null) return;

        // Get direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Smoothly rotate toward the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

        // Move forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void FindTarget()
    {
        // Find the closest enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
