using UnityEngine;

public class Winner_CV : MonoBehaviour
{
    public GameObject gameWinCanvas;     
    public GameObject missionCanvas;     
    public GameObject noticeCanvas;     

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (missionCanvas != null && missionCanvas.activeSelf)
        {
            if (noticeCanvas != null)
                noticeCanvas.SetActive(true);
        }
        else
        {
            if (gameWinCanvas != null)
                gameWinCanvas.SetActive(true);

            Time.timeScale = 0f;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);

            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                Destroy(bullet);
        }
    }
}
