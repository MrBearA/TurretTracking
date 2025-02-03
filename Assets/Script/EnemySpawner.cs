using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPointQuadratic;
    public Transform spawnPointCubic;
    public Transform targetLocation;
    public GameObject enemyPrefab;

    public float spawnInterval = 3f;
    public int playerHP = 10;
    public TextMeshProUGUI hpText;
    public GameObject failUI;

    void Start()
    {
        UpdateHPUI();
        InvokeRepeating(nameof(SpawnEnemyQuadratic), 0, spawnInterval);
        InvokeRepeating(nameof(SpawnEnemyCubic), 1.5f, spawnInterval);
    }

    void SpawnEnemyQuadratic()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPointQuadratic.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().Init(targetLocation, Enemy.MovementType.Quadratic, this);
    }

    void SpawnEnemyCubic()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPointCubic.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().Init(targetLocation, Enemy.MovementType.Cubic, this);
    }

    public void TakeDamage()
    {
        playerHP--;
        UpdateHPUI();
        if (playerHP <= 0)
        {
            failUI.SetActive(true); // Show fail screen
            Time.timeScale = 0; // Pause game
        }
    }

    void UpdateHPUI()
    {
        hpText.text = "HP: " + playerHP;
    }
}
