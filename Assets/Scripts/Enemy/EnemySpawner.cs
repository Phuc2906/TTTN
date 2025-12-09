using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRate = 5f;
    public float spawnRadius = 5f;
    public int maxEnemiesAlive = 10;
    public int totalSpawnLimit = 100;         // mục tiêu số kill
    public EnemyCountDisplay enemyCountDisplay;

    private int currentAliveCount = 0;
    private int totalSpawned = 0;             // số đã spawn (tổng)
    private int totalKilled = 0;              // số đã kill
    private bool isSpawning = true;           // flag điều khiển coroutine

    void Start()
    {
        // Nếu có save → load kill, nếu đã hoàn thành thì dừng spawn
        if (PlayerPrefs.HasKey("SavedKillCount"))
        {
            totalKilled = PlayerPrefs.GetInt("SavedKillCount");
        }
        else
        {
            totalKilled = 0;
        }

        // Nếu đã đạt mục tiêu kill → không spawn nữa
        if (totalKilled >= totalSpawnLimit)
        {
            isSpawning = false;
            totalKilled = Mathf.Min(totalKilled, totalSpawnLimit);
        }

        if (enemyCountDisplay != null)
            enemyCountDisplay.UpdateCount(totalKilled, totalSpawnLimit);

        if (isSpawning)
            StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            // Nếu đã đạt mục tiêu kill -> dừng spawn
            if (totalKilled >= totalSpawnLimit)
            {
                isSpawning = false;
                yield break;
            }

            // Nếu còn chỗ để spawn và tổng spawn chưa vượt limit
            // và số alive hiện tại chưa vượt max
            if (currentAliveCount < maxEnemiesAlive && totalSpawned < totalSpawnLimit)
            {
                // thêm check để tránh spawn vượt do race
                if (totalSpawned >= totalSpawnLimit || totalKilled >= totalSpawnLimit)
                {
                    isSpawning = false;
                    yield break;
                }

                Vector3 spawnPos = GetRandomSpawnPosition2D();
                if (spawnPos != Vector3.zero)
                {
                    // một check cuối cùng trước instantiate
                    int aliveProjected = currentAliveCount + 1;
                    int spawnsRemaining = totalSpawnLimit - totalSpawned;
                    if (spawnsRemaining <= 0 || totalKilled >= totalSpawnLimit)
                    {
                        isSpawning = false;
                        yield break;
                    }

                    // instantiate an toàn
                    GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                    EnemyHealth eh = newEnemy.GetComponent<EnemyHealth>();
                    if (eh != null)
                        eh.spawner = this;
                        currentAliveCount++;
                    totalSpawned++;

                    if (enemyCountDisplay != null)
                        enemyCountDisplay.UpdateCount(totalKilled, totalSpawnLimit);
                }
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    Vector3 GetRandomSpawnPosition2D()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
            if (Vector2.Distance(randomPos, player.position) < 5f) continue;
            return new Vector3(randomPos.x, randomPos.y, 0);
        }
        return Vector3.zero;
    }

    public void OnEnemyKilled()
    {
        // luôn cập nhật số alive
        currentAliveCount = Mathf.Max(0, currentAliveCount - 1);

        // Nếu đã đạt limit thì không tăng thêm kill
        if (totalKilled >= totalSpawnLimit)
        {
            // chỉ cập nhật UI (giữ nguyên 100/100)
            if (enemyCountDisplay != null)
                enemyCountDisplay.UpdateCount(totalKilled, totalSpawnLimit);
            return;
        }

        totalKilled++;

        // đảm bảo không vượt quá limit (nếu race làm tăng vượt)
        if (totalKilled >= totalSpawnLimit)
        {
            totalKilled = totalSpawnLimit;
            isSpawning = false;
            Debug.Log("Reached kill target. Stopping further spawns. totalKilled = " + totalKilled);
        }

        if (enemyCountDisplay != null)
            enemyCountDisplay.UpdateCount(totalKilled, totalSpawnLimit);
    }

    // Hàm để SaveGameHandler lấy kill khi Save
    public int GetKillCount()
    {
        return totalKilled;
    }
}