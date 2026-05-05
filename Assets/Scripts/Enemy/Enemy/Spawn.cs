using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Boss")]
    public Transform boss;

    [Header("Prefabs")]
    public GameObject obj1Prefab;
    public GameObject obj2Prefab;

    [Header("Spawn settings")]
    public float spawnRadius = 5f;
    public float spawnDelay = 5f;

    [Header("Y offset nhẹ")]
    public float yOffset = 0.2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnWave), spawnDelay, spawnDelay);
    }

    void SpawnWave()
    {
        if (boss == null) return;

        int unitCount = Random.Range(2, 4);

        for (int i = 0; i < unitCount; i++)
        {
            Vector2 rand = Random.insideUnitCircle * spawnRadius;
            Vector3 basePos = boss.position + new Vector3(rand.x, rand.y, 0);

            GameObject obj1 = Instantiate(obj1Prefab);
            GameObject obj2 = Instantiate(obj2Prefab);

            obj1.transform.position = basePos;
            obj2.transform.position = basePos + new Vector3(0, yOffset, 0);

            obj1.transform.rotation = Quaternion.identity;
            obj2.transform.rotation = Quaternion.identity;

            obj1.SetActive(true);
            obj2.SetActive(false);

            Skill skill = obj1.GetComponent<Skill>();
            if (skill != null)
            {
                skill.obj2 = obj2;
            }
        }
    }
}