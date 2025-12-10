using UnityEngine;

public class Close_Instruct : MonoBehaviour
{
    [Header("Canvas hướng dẫn")]
    public GameObject guideCanvas;

    [Header("Tên key để lưu PlayerPrefs")]
    public string saveKey = "GuideShown";

    void Start()
    {
        if (PlayerPrefs.GetInt(saveKey, 0) == 1)
        {
            guideCanvas.SetActive(false);
        }
        else
        {
            guideCanvas.SetActive(true);
        }
    }
    public void CloseGuide()
    {
        guideCanvas.SetActive(false);
        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();
    }
}
