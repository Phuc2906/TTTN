using UnityEngine;

public class Close_CheckCanvas : MonoBehaviour
{
    public GameObject Canvas_A;     
    public GameObject Canvas_B;     
    public Interact_02 interact_02Script; 

    public void CanvasOnClick()
    {
        if (Canvas_A != null)
            Canvas_A.SetActive(false);

        if (Canvas_B != null)
            Canvas_B.SetActive(true);

        if (interact_02Script != null)
            interact_02Script.NotifyCheckCanvasOpened();
    }
}
