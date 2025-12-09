using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [Header("Scene hiện tại (VD: Level1, MainMenu)")]
    public string currentSceneName;

    [Header("Scene muốn chuyển đến (VD: Setting hoặc Shop)")]
    public string targetSceneName;

    [Header("Scene này có cần lưu Pause không? (chỉ tick cho Setting)")]
    public bool savePauseState = false;

    public void LoadTargetScene()
    {
        PlayerPrefs.SetString("LastScene", currentSceneName);
        
        PlayerMove player = FindObjectOfType<PlayerMove>();
        if (player != null) player.SavePosition();

        if(savePauseState && currentSceneName == "Easy_Level1" && GameManager.instance != null)
            PlayerPrefs.SetInt("WasPaused", GameManager.instance.IsPaused() ? 1 : 0);
        else
            PlayerPrefs.SetInt("WasPaused", 0); 
       
        SceneManager.LoadScene(targetSceneName);
    }
}
