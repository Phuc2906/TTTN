using UnityEngine;

public class CoinToAdd : MonoBehaviour
{
    public int coinToAdd = 50; 
    public GameObject Canvas;

    public void AddCoin()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(coinToAdd);
            Canvas.SetActive(false);
        }
    }
}