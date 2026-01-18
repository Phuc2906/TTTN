using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject CanvasA;
    public GameObject CanvasB;
    public GameObject CanvasC;

    public void OnGetClick()
    {
        CanvasA.SetActive(false);
        CanvasB.SetActive(true);
        CanvasC.SetActive(true);
    }
}
