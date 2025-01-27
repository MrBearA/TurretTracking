using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform player; // Assign the player's Transform
    public GameObject projectilePrefab; // The projectile prefab
    public Transform firePoint; // Where the projectile spawns
    public float turnSpeed = 5f; // Speed of turret rotation
    public float detectionRange = 15f; // Range at which turret fires
    public float fireCooldown = 2f; // Cooldown time between shots
    public float firingAngleThreshold = 10f; // Angle threshold to fire

    private bool canFire = true;

    void Update()
    {
        if (player == null) return;

        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Rotate turret to track the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Calculate the distance and angle to the player
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Fire only if the player is within range and angle threshold
        if (distanceToPlayer <= detectionRange && angleToPlayer <= firingAngleThreshold && canFire)
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        canFire = false;

        // Instantiate and launch the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 20f; // Adjust speed as needed

        StartCoroutine(FireCooldownRoutine());
    }

    IEnumerator FireCooldownRoutine()
    {
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}
