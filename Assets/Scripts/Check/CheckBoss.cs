using UnityEngine;

public class CheckBoss : MonoBehaviour
{
    public int bossID = 1;
    public GameObject targetObject;     
    public GameObject warningCanvas; 
    public GameObject Canvas;   
    public int requiredLevel = 15;      

    private string saveKey;

    void Start()
    {
        saveKey = "BossDestroyed_" + bossID;

        if (PlayerPrefs.GetInt(saveKey, 0) == 1)
        {
            Destroy(targetObject);
        }

        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        int playerLevel = PlayerExpManager.Instance.GetLevel();

        if (playerLevel >= requiredLevel)
        {
            Destroy(targetObject);
            Canvas.SetActive(true);

            PlayerPrefs.SetInt(saveKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            if (warningCanvas != null)
                warningCanvas.SetActive(true);
        }
    }
}