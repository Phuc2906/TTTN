using UnityEngine;

public class Close_02 : MonoBehaviour
{
    public GameObject canvasToClose;   
    public Interact_02 interact_02Script;    

    public void CanvasOnClick()
    {
        if (canvasToClose != null)
            canvasToClose.SetActive(false);

        if (interact_02Script != null)
            interact_02Script.StopInteract();
    }
}
