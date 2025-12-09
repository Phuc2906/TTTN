using UnityEngine;

public class Canvas_Level : MonoBehaviour
{
    public GameObject settingCanvas;

    void Start()
    {
        if(PlayerPrefs.GetInt("CanvasState", 0) == 1)
        {
            settingCanvas.SetActive(true);
            PlayerPrefs.SetInt("CanvasState", 0); 
        }
    }
}
