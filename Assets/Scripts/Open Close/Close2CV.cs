using UnityEngine;

public class Close2CV : MonoBehaviour
{
    public GameObject Canvas_A;
    public GameObject Canvas_B; 
    public GameObject Canvas_C;
    

    public void CanvasOnClick()
    {
        Canvas_A.SetActive(false);
        Canvas_B.SetActive(false);
        Canvas_C.SetActive(true);
    }
}
