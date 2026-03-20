using UnityEngine;

public class CanvasState : MonoBehaviour
{
    [Header("PlayerPrefs")]
    public string canvasPrefKey = "Canvas_01";

    void Awake()
    {
        bool isActive = PlayerPrefs.GetInt(canvasPrefKey, 1) == 1;
        gameObject.SetActive(isActive);
    }

    void OnEnable()
    {
        PlayerPrefs.SetInt(canvasPrefKey, 1);
        PlayerPrefs.Save();
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt(canvasPrefKey, 0);
        PlayerPrefs.Save();
    }
}
