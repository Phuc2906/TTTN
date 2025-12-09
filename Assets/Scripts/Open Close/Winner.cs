using UnityEngine;

public class Winner : MonoBehaviour
{
    public GameObject gameWinCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (gameWinCanvas != null)
                gameWinCanvas.SetActive(true);

            PlayerPrefs.SetInt("Level1Clear", 1);
            PlayerPrefs.Save();

            Time.timeScale = 0f;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);

            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                Destroy(bullet);
        }
    }
}
