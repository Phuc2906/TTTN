using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject Canvas_A; 
    

    public void CloseCanvasOnClick()
    {
        Canvas_A.SetActive(false); 
    }
}
