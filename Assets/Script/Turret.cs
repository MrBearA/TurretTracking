using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectilePrefab; // Bullet Prefab
    public Transform firePoint; // Where bullets spawn
    public float turnSpeed = 5f; // Rotation speed
    public float detectionRange = 15f; // How far it detects enemies
    public float fireCooldown = 2f; // Time between shots
    public float firingAngleThreshold = 10f; // Only fire if enemy is within this angle

    private Transform targetEnemy; // The enemy it is currently tracking
    private bool canFire = true;

    void Update()
    {
        FindClosestEnemy();

        if (targetEnemy == null) return;

        // Rotate turret toward the enemy
        Vector3 directionToEnemy = targetEnemy.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Fire only if the enemy is in range and within the angle threshold
        float distanceToEnemy = directionToEnemy.magnitude;
        float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

        if (distanceToEnemy <= detectionRange && angleToEnemy <= firingAngleThreshold && canFire)
        {
            FireProjectile();
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            targetEnemy = null;
            return;
        }

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        targetEnemy = closestEnemy;
    }

    void FireProjectile()
    {
        canFire = false;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 20f;

        StartCoroutine(FireCooldownRoutine());
    }

    IEnumerator FireCooldownRoutine()
    {
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}
