using UnityEngine;

public class CoinSave : MonoBehaviour
{
    [Header("ID duy nhất cho coin")]
    public int coinID; 
    string saveKey;

    void Awake()
    {
        saveKey = "Coin_" + coinID;

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
