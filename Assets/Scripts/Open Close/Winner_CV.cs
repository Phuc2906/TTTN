using UnityEngine;

public class Winner_CV : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameWinCanvas;     
    public GameObject missionCanvas;     
    public GameObject noticeCanvas;     

    [Header("Level Save")]
    public string levelClearKey;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (missionCanvas != null && missionCanvas.activeSelf)
        {
            if (noticeCanvas != null)
                noticeCanvas.SetActive(true);

            return;
        }

        if (gameWinCanvas != null)
            gameWinCanvas.SetActive(true);

        if (!string.IsNullOrEmpty(levelClearKey))
        {
            PlayerPrefs.SetInt(levelClearKey, 1);
            PlayerPrefs.Save();
        }

        Time.timeScale = 0f;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            Destroy(bullet);
    }
}
