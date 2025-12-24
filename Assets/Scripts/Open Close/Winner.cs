using UnityEngine;

public class Winner : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameWinCanvas;

    [Header("Level")]
    public string levelClearKey;

    private bool isWin = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isWin) return;
        if (!other.CompareTag("Player")) return;

        isWin = true;

        if (gameWinCanvas != null)
            gameWinCanvas.SetActive(true);

        if (!string.IsNullOrEmpty(levelClearKey))
        {
            PlayerPrefs.SetInt(levelClearKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Không có dữ liệu để lưu.");
        }

        Time.timeScale = 0f;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            Destroy(bullet);
    }
}
