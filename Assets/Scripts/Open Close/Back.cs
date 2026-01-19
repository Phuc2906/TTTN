using UnityEngine;
using UnityEngine.SceneManagement;
public class Back : MonoBehaviour
{
    public void LoadSceneMainMenu()
    {
        string lastScene = PlayerPrefs.GetString("LastScene", "MainMenu");

        SceneManager.LoadScene(lastScene);
    }
}
