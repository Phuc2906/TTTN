using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;

    public GameObject player;
    public GameObject pauseGameCanvas;
    public GameObject gameoverCanvas;
    public GameObject gamewinCanvas;
    public Button restartButton;
    public Button exitButton;

    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("WasPaused", 0) == 1)
        {
            isPaused = true;
            pauseGameCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            if (pauseGameCanvas != null) pauseGameCanvas.SetActive(false);
        }

        if (gameoverCanvas != null) gameoverCanvas.SetActive(false);
        if (gamewinCanvas != null) gamewinCanvas.SetActive(false);

        PlayerPrefs.SetInt("WasPaused", 0); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !gameoverCanvas.activeSelf && !gamewinCanvas.activeSelf)
        {
            TogglePause();
        }
    }
    void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseGameCanvas != null)
            pauseGameCanvas.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public bool IsPaused() => isPaused;

    public void GameOver()
    {
        Time.timeScale = 0f;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        if (gameoverCanvas != null) gameoverCanvas.SetActive(true);
    }

    public void GameWinner()
    {
        Time.timeScale = 0f;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        if (gamewinCanvas != null) gamewinCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}