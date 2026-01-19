using UnityEngine;
using UnityEngine.SceneManagement;

public class Agree : MonoBehaviour
{
    [Header("Scene hiện tại")]
    public string currentSceneName;

    [Header("Scene so sánh")]
    public string compareSceneName;

    [Header("Scene muốn chuyển đến")]
    public string targetSceneName;

    [Header("Pause")]
    public bool savePauseState = false;

    public void LoadAgreeScene()
    {
        PlayerPrefs.SetString("LastScene", currentSceneName);

        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        if (playerMove != null)
            playerMove.SavePosition();

        if (savePauseState && currentSceneName == compareSceneName && GameManager.instance != null)
            PlayerPrefs.SetInt("WasPaused", GameManager.instance.IsPaused() ? 1 : 0);
        else
            PlayerPrefs.SetInt("WasPaused", 0);

        PlayerPrefs.Save();

        SceneManager.LoadScene(targetSceneName);
    }
}
