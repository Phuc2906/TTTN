using UnityEngine;

public class EnemySave : MonoBehaviour
{
    [Header("ID duy nhất cho Enemy")]
    public int enemyID;

    private string saveKey;

    void Awake()
    {
        saveKey = "Enemy_" + enemyID;

        if (PlayerPrefs.GetInt(saveKey, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
}
