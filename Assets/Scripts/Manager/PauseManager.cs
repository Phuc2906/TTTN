using UnityEngine;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour
{
    [Header("Canvases")]
    public List<GameObject> pauseCanvases = new List<GameObject>();

    void Update()
    {
        if (IsAnyCanvasActive())
            PauseGame();
        else
            ResumeGame();
    }

    bool IsAnyCanvasActive()
    {
        foreach (var canvas in pauseCanvases)
        {
            if (canvas != null && canvas.activeInHierarchy)
                return true;
        }
        return false;
    }

    void PauseGame()
    {
        if (Time.timeScale != 0f)
            Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;
    }
}
