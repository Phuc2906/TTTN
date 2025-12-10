using UnityEngine;

public class Reward : MonoBehaviour
{
    public int rewardValue = 1;
    public GameObject x2Canvas;   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (x2Canvas != null && x2Canvas.activeSelf)
                rewardValue = 2;       
            else
                rewardValue = 1;        

            CoinManager.Instance.AddCoin(rewardValue);

            Destroy(gameObject);
        }
    }
}
