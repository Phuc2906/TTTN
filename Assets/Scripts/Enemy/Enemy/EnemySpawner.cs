using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Center (object cố định)")]
    public Transform spawnCenter;

    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("UI")]
    public EnemyCountDisplay enemyCountDisplay;

    [Header("Spawn Settings")]
    public float spawnRate = 3f;
    public float spawnRadius = 8f;
    public float innerSafeRadius = 3f;
    public int maxEnemiesAlive = 10;
    public int spawnLimit = 100;

    [Header("Collision Check")]
    public LayerMask blockMask;
    public float checkRadius = 0.6f;

    private int aliveCount;
    private int totalSpawned;

    void Start()
    {
        enemyCountDisplay?.UpdateCount(
            aliveCount,
            maxEnemiesAlive,
            totalSpawned,
            spawnLimit
        );

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (totalSpawned < spawnLimit)
        {
            if (aliveCount < maxEnemiesAlive)
            {
                Vector3 spawnPos;
                if (TryGetSpawnPos(out spawnPos))
                {
                    GameObject e = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                    EnemyHealth hp = e.GetComponent<EnemyHealth>();
                    if (hp) hp.spawner = this;

                    aliveCount++;
                    totalSpawned++;

                    enemyCountDisplay?.UpdateCount(
                        aliveCount,
                        maxEnemiesAlive,
                        totalSpawned,
                        spawnLimit
                    );
                }
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    bool TryGetSpawnPos(out Vector3 result)
    {
        Vector2 center = spawnCenter.position;

        for (int i = 0; i < 20; i++)
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            float dist = Random.Range(innerSafeRadius, spawnRadius);
            Vector2 pos = center + dir * dist;

            if (Physics2D.OverlapCircle(pos, checkRadius, blockMask))
                continue;

            result = new Vector3(pos.x, pos.y, 0);
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void OnEnemyKilled()
    {
        aliveCount = Mathf.Max(0, aliveCount - 1);

        enemyCountDisplay?.UpdateCount(
            aliveCount,
            maxEnemiesAlive,
            totalSpawned,
            spawnLimit
        );
    }
}
