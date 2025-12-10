using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform player;
    public EnemyCountDisplay enemyCountDisplay;

    [Header("Spawn Settings")]
    public float spawnRate = 3f;
    public float spawnRadius = 6f;
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
                Vector3 spawnPos = GetRandomSpawnPos();
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

    Vector3 GetRandomSpawnPos()
    {
        Vector2 rp = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
        return new Vector3(rp.x, rp.y, 0);
    }

    public void OnEnemyKilled()
    {
        aliveCount = Mathf.Max(0, aliveCount - 1);

        enemyCountDisplay?.UpdateCount(aliveCount, maxEnemiesAlive, totalSpawned, spawnLimit);
    }
}
