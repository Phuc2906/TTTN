using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting_Hard_02 : MonoBehaviour
{
    [Header("Tên Scene hiện tại (ghi đúng trong Build Settings)")]
    public string currentSceneName; 

    public void OpenSetting()
    {
        PlayerPrefs.SetString("LastScene", currentSceneName);
        
        PlayerMove player = FindObjectOfType<PlayerMove>();
        if (player != null) player.SavePosition();

        if(currentSceneName == "Hard_Level2" && GameManager.instance != null)
        {
            PlayerPrefs.SetInt("WasPaused", GameManager.instance.IsPaused() ? 1 : 0);
        }
        else
        {
            PlayerPrefs.SetInt("WasPaused", 0);
        }

        SceneManager.LoadScene("Setting");
    }
}
