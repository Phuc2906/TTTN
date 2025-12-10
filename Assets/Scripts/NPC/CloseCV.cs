using UnityEngine;

public class CloseCV : MonoBehaviour
{
    public GameObject canvasToClose;   
    public Communicate communicateScript;    

    public void CanvasOnClick()
    {
        if (canvasToClose != null)
            canvasToClose.SetActive(false);

        if (communicateScript != null)
            communicateScript.StopInteract();
    }
}
