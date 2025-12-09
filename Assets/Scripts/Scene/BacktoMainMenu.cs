using UnityEngine;
using UnityEngine.SceneManagement;
public class BacktoMainMenu : MonoBehaviour
{
    public void Back()
    {
        string lastScene = PlayerPrefs.GetString("LastScene", "MainMenu");

        SceneManager.LoadScene(lastScene);
    }
}
