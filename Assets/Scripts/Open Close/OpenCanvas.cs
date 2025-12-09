using UnityEngine;

public class OpenCanvas : MonoBehaviour
{
    public GameObject Canvas_A; 
    public GameObject Canvas_B; 
    

    public void CanvasOnClick()
    {
        Canvas_A.SetActive(true);
        Canvas_B.SetActive(false); 
    }
}
