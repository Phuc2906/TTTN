using UnityEngine;

public class Reward : MonoBehaviour
{
    public int rewardValue = 1;
    public GameObject x2Canvas;

    private CoinSave coinSave;

    void Awake()
    {
        coinSave = GetComponent<CoinSave>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        rewardValue = (x2Canvas != null && x2Canvas.activeSelf) ? 2 : 1;

        CoinManager.Instance.AddCoin(rewardValue);

        if (coinSave != null)
            coinSave.Collect();
        else
            Destroy(gameObject);
    }
}
