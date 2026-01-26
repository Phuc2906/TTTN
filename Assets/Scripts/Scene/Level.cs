using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public string mode;
    public int levelIndex;
    public Button button;

    [Header("Complete Canvas")]
    public GameObject completeCanvas;

    void Start()
    {
        bool unlocked = levelIndex == 1 ||
            PlayerPrefs.GetInt(mode + "_Level" + (levelIndex - 1), 0) == 1;

        button.interactable = unlocked;
    }

    public void PlayLevel()
    {
        string key = mode + "_Level" + levelIndex;

        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            if (completeCanvas != null)
                completeCanvas.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(mode + "_Level" + levelIndex);
        }
    }
}
