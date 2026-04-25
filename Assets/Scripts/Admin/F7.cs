using UnityEngine;

public class AdminToggleUI : MonoBehaviour
{
    [Header("UI Canvas")]
    public GameObject adminCanvas;

    private bool isOpen = false;

    void Start()
    {
        if (adminCanvas != null)
            adminCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
        {
            ToggleCanvas();
        }
    }

    void ToggleCanvas()
    {
        isOpen = !isOpen;

        if (adminCanvas != null)
        {
            adminCanvas.SetActive(isOpen);
        }
    }
}