using UnityEngine;
using System.Collections.Generic;

public class ClickCanvas : MonoBehaviour
{
    [Header("Canvas cần bật")]
    public List<GameObject> canvasesToOpen;

    [Header("Canvas cần tắt")]
    public List<GameObject> canvasesToClose;

    public void Click()
    {
        bool anyOpen = false;

        foreach (GameObject canvas in canvasesToOpen)
        {
            if (canvas != null && canvas.activeSelf)
            {
                anyOpen = true;
                break;
            }
        }

        if (!anyOpen)
        {
            foreach (GameObject canvas in canvasesToOpen)
            {
                if (canvas != null)
                    canvas.SetActive(true);
            }

            foreach (GameObject canvas in canvasesToClose)
            {
                if (canvas != null)
                    canvas.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject canvas in canvasesToOpen)
            {
                if (canvas != null)
                    canvas.SetActive(false);
            }
        }
    }
}
