using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Center")]
    public Transform spawnCenter;    

    [Header("References")]
    public GameObject enemyPrefab;
    public EnemyCountDisplay enemyCountDisplay;

    [Header("Spawn Settings")]
    public float spawnRate = 3f;
    public float spawnRadius = 6f;        
    public float minSpawnDistance = 2.5f; 
    public int maxEnemiesAlive = 10;
    public int spawnLimit = 100;

    private int aliveCount = 0;
    private int totalSpawned = 0;
    private bool canSpawn = true;

    void Start()
    {
        enemyCountDisplay?.UpdateCount(aliveCount, maxEnemiesAlive, totalSpawned, spawnLimit);
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (canSpawn)
        {
            if (aliveCount < maxEnemiesAlive && totalSpawned < spawnLimit)
            {
                Vector3 spawnPos = GetValidSpawnPos();
                GameObject e = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                EnemyHealth hp = e.GetComponent<EnemyHealth>();
                if (hp != null) hp.spawner = this;

                aliveCount++;
                totalSpawned++;

                enemyCountDisplay?.UpdateCount(aliveCount, maxEnemiesAlive, totalSpawned, spawnLimit);
            }
            else if (totalSpawned >= spawnLimit)
            {
                canSpawn = false;
                Debug.Log("Đạt giới hạn spawn!");
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    Vector3 GetValidSpawnPos()
    {
        Vector2 center = spawnCenter.position;

        for (int i = 0; i < 10; i++) 
        {
            Vector2 randomPos = center + Random.insideUnitCircle.normalized *
                                Random.Range(minSpawnDistance, spawnRadius);

            return new Vector3(randomPos.x, randomPos.y, 0);
        }

        return new Vector3(center.x + spawnRadius, center.y, 0);
    }

    public void OnEnemyKilled()
    {
        aliveCount = Mathf.Max(0, aliveCount - 1);
        enemyCountDisplay?.UpdateCount(aliveCount, maxEnemiesAlive, totalSpawned, spawnLimit);
    }
}
