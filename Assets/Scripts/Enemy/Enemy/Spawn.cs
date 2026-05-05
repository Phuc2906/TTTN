using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Boss")]
    public Transform boss;

    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Cài đặt spawn")]
    public float spawnDelay = 5f;
    public int minSpawn = 2;
    public int maxSpawn = 3;
    public float spawnRadius = 5f;

    [Header("ID bắt đầu cho quái spawn")]
    public int startID = 2000;

    private int currentSpawnID;

    void Start()
    {
        currentSpawnID = startID;

        if (boss == null)
        {
            GameObject b = GameObject.FindWithTag("Boss");
            if (b != null) boss = b.transform;
        }

        InvokeRepeating(nameof(SpawnWave), spawnDelay, spawnDelay);
    }

    void SpawnWave()
    {
        if (boss == null || enemyPrefab == null) return;

        int amount = Random.Range(minSpawn, maxSpawn + 1);

        for (int i = 0; i < amount; i++)
        {
            Vector2 rand = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = boss.position + new Vector3(rand.x, rand.y, 0);

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.parent = null;
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = Quaternion.identity;

            enemy.transform.localScale = Vector3.one;

            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.enemyID = currentSpawnID;
                currentSpawnID++;
            }

        }
    }
}