using UnityEngine;

public class OpenCV : MonoBehaviour
{
    public GameObject canvasToOpen;   
    public Communicate communicateScript;    

    public void CanvasOnClick()
    {
        if (canvasToOpen != null)
            canvasToOpen.SetActive(true);

        if (communicateScript != null)
            communicateScript.StopInteract();
    }
}
