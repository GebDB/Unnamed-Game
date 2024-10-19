using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform[] spawnPoints;
    private int enemyCount = 0;
    [SerializeField] private int maxEnemyCount = 100;
    [SerializeField] private float minSpawnTime = 1;
    [SerializeField] private float maxSpawnTime = 2;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void Update()
    {

    }
    // Spawn at specified locations with a random time delay.
    IEnumerator SpawnEnemy()
    {
        while (enemyCount <= maxEnemyCount)
        {
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPoint = spawnPoints[randomIndex].position;
            Instantiate(enemy, spawnPoint, Quaternion.identity);
            enemyCount++;
        }

    }
}
