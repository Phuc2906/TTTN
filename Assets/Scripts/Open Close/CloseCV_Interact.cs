using UnityEngine;

public class CloseCV_Interact : MonoBehaviour
{
    public GameObject canvasToClose;   
    public Interact interactScript;    

    public void CanvasOnClick()
    {
        if (canvasToClose != null)
            canvasToClose.SetActive(false);

        if (interactScript != null)
            interactScript.StopInteract();
    }
}
