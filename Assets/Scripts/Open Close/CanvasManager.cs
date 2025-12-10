using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject Canvas_A;
    public GameObject Canvas_B;
    public GameObject Canvas_C; 
    

    public void CloseCanvasOnClick()
    {
        Canvas_A.SetActive(false); 
        Canvas_B.SetActive(true);
        Canvas_C.SetActive(true);
    }
}
