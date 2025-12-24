using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public string mode;
    public int levelIndex;
    public Button button;

    void Start()
    {
        bool unlocked = levelIndex == 1 ||
            PlayerPrefs.GetInt(mode + "_Level" + (levelIndex - 1), 0) == 1;

        button.interactable = unlocked;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(mode + "_Level" + levelIndex);
    }
}
