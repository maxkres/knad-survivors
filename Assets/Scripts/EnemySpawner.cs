using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int baseCount = 5;
    public float spawnInterval = 1f;

    public Vector2 spawnAreaMin = new Vector2(-10, -10);
    public Vector2 spawnAreaMax = new Vector2(10, 10);

    public static int spawnedEnemies = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int enemyCount = baseCount * PlayerStats.lvl;

            if (spawnedEnemies < enemyCount)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        float x = UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(x, y);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.InitializeEnemy();
        }

        spawnedEnemies++;
    }
}