using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [Header("Tên Scene hiện tại (ghi đúng trong Build Settings)")]
    public string currentSceneName; 

    public void OpenSetting()
    {
        PlayerPrefs.SetString("LastScene", currentSceneName);
        
        PlayerMove player = FindObjectOfType<PlayerMove>();
        if (player != null) player.SavePosition();

        if(currentSceneName == "Easy_Level1" && GameManager.instance != null)
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
