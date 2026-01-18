using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [Header("Tên Scene hiện tại (ghi đúng trong Build Settings)")]
    public string currentSceneName; 
    [Header("Tên Scene đã lưu (ghi đúng trong Build Settings)")]
    public string savedSceneName;

    public void OpenSetting()
{

    PlayerPrefs.SetString("LastScene", currentSceneName);

    PlayerMove player = FindObjectOfType<PlayerMove>();
    if (player != null) player.SavePosition();

    if (currentSceneName == savedSceneName && GameManager.instance != null)
    {
        PlayerPrefs.SetInt("WasPaused", GameManager.instance.IsPaused() ? 1 : 0);
    }
    else
    {
        PlayerPrefs.SetInt("WasPaused", 0);
    }

    PlayerPrefs.Save();
    SceneManager.LoadScene("Setting");
}
}
