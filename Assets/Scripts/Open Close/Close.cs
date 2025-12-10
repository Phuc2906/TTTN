using UnityEngine;

public class Close : MonoBehaviour
{
    public GameObject Canvas_A;
    public GameObject Canvas_B; 
    

    public void CanvasOnClick()
    {
        Canvas_A.SetActive(false);
        Canvas_B.SetActive(true);
    }
}
