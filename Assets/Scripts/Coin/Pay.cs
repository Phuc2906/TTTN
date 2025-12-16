using UnityEngine;

public class Pay : MonoBehaviour
{
    [Header("Cấu hình")]
    public int cost = 10;                
    public GameObject objectToDisable;
    public GameObject communicateCanvas;    
    public GameObject notEnoughCanvas;    

    public void OnButtonClick()
    {
        if (CoinManager.Instance == null) return;

        bool success = CoinManager.Instance.SpendCoin(cost);

        if (success)
        {
            if (objectToDisable != null)
                objectToDisable.SetActive(false);
            if (communicateCanvas != null)
                communicateCanvas.SetActive(false);
        }
        else
        {
            if (notEnoughCanvas != null)
                notEnoughCanvas.SetActive(true);
        }
    }
}
