using UnityEngine;

public class CloseManager : MonoBehaviour
{
    [Header("Canvas CẦN BẬT")]
    public GameObject[] canvasToEnable;

    [Header("Canvas CẦN TẮT")]
    public GameObject[] canvasToDisable;

    public void Apply()
    {
        foreach (GameObject canvas in canvasToEnable)
        {
            if (canvas != null)
                canvas.SetActive(true);
        }

        foreach (GameObject canvas in canvasToDisable)
        {
            if (canvas != null)
                canvas.SetActive(false);
        }
    }
}
