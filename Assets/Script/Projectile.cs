using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f; // How long the projectile lives

    void Start()
    {
        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the projectile collides with the player
        if (other.CompareTag("Player"))
        {
            // Restart the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
