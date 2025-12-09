using UnityEngine;

public class Close : MonoBehaviour
{
    [Header("Canvas cần quản lý")]
    public GameObject targetCanvas;

    private const string key = "InstructClosed"; 

    void Start()
    {
        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            targetCanvas.SetActive(false);
        }
        else
        {
            targetCanvas.SetActive(true);
        }
    }

    public void CloseCanvas()
    {
        targetCanvas.SetActive(false);

        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }
}
